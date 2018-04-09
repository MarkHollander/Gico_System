using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Caching.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Gico.FrontEndAppService.Filters
{
    public class OutputCacheAttribute : TypeFilterAttribute
    {
        public OutputCacheAttribute(int timeoutSecond) : base(typeof(OutputCacheFilter))
        {
            this.Arguments = new object[] { timeoutSecond };
        }

    }
    public class OutputCacheFilter : IAsyncActionFilter
    {
        private readonly ICurrentContext _currentContext;
        private readonly IDistributedCache _distributedCache;
        public OutputCacheFilter(int timeoutSecond, ICurrentContext currentContext, IDistributedCache distributedCache)
        {
            TimeoutSecond = timeoutSecond;
            _currentContext = currentContext;
            _distributedCache = distributedCache;
        }

        private int TimeoutSecond { get; }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestUrl = context.HttpContext.Request.GetEncodedUrl();
            var cacheKey = Md5(requestUrl);
            var cachedResult = await _distributedCache.GetAsync(cacheKey);
            if (cachedResult != null)
            {
                //string html=Common.Serialize.pr
                //var httpResponse = context.HttpContext.Response;

                //var responseStream = httpResponse.Body;
                //responseStream.Seek(0, SeekOrigin.Begin);
                //if (responseStream.Length <= cachedResult.Length)
                //{
                //    responseStream.SetLength((long)cachedResult.Length << 1);
                //}
                //using (var writer = new StreamWriter(responseStream, Encoding.UTF8, 4096, true))
                //{
                //    writer.Write(cachedResult);
                //    writer.Flush();
                //    responseStream.Flush();
                //    context.Result = new ContentResult { Content = cachedResult };
                //}

            }
            else
            {
                var resultContext = await next();
                var originalBodyStream = resultContext.HttpContext.Response.Body;
                //var buffer = new byte[originalBodyStream.Length];
                //await originalBodyStream.ReadAsync(buffer, 0, buffer.Length);

                //using (var streamReader = new StreamReader(responseStream, Encoding.UTF8, true, 512, true))
                //{
                //    var toCache = streamReader.ReadToEnd();
                //    await _distributedCache.SetAsync(cacheKey, Common.Serialize.ProtoBufSerialize(toCache));
                //}
            }

            //context.Result = new ContentResult()
            //{
            //    Content = "1111",
            //    StatusCode = (int)HttpStatusCode.OK
            //};
            context.HttpContext.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                MaxAge = TimeSpan.FromSeconds(0)
            };
            context.HttpContext.Response.Headers[HeaderNames.Pragma] = new string[] { "no-cache" };
            context.HttpContext.Response.Headers[HeaderNames.Vary] = new string[] { "Accept-Encoding,User-Agent" };
        }
        public string Md5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }
    }
}