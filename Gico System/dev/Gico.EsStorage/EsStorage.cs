using Gico.Config;
using Gico.Domains;
using Gico.Resilience.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gico.EsStorage
{
    public interface IEsStorage
    {
        Task<string> Add<T>(IndexEs<T> indexObject);
        Task<string> Add<T>(IndexEs<T>[] indexEses);
        Task<string> Search(EnumDefine.EsIndexName indexName, EnumDefine.EsIndexType indexType, string script);
    }
    public class EsStorage : IEsStorage
    {
        protected readonly IHttpClient HttpClient;
        protected static readonly string Url = ConfigSettingEnum.EsUrl.GetConfig();

        public EsStorage(IHttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<string> Add<T>(IndexEs<T> indexObject)
        {
            try
            {
                var url = (Url.EndsWith("/") ? $"{Url}{indexObject.IndexAddUrl}" : $"{Url}/{indexObject.IndexAddUrl}").ToLower();
                if (indexObject.ParentId != null)
                {
                    url += "?parent=" + indexObject.ParentId;
                }
                string content = indexObject.IndexAddScript;
                using (var response = await HttpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var p = await response.Content.ReadAsStringAsync();
                        return p;
                    }
                    else
                    {
                        throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                e.Data["Param"] = indexObject;
                throw e;
            }
            catch (Exception e)
            {
                e.Data["Param"] = indexObject;
                throw e;
            }
        }

        public async Task<string> Add<T>(IndexEs<T>[] indexEses)
        {
            try
            {
                var url = (Url.EndsWith("/") ? $"{Url}{EnumDefine.EsMethodName._bulk}" : $"{Url}/{EnumDefine.EsMethodName._bulk}").ToLower();
                string content = string.Join("", indexEses.Select(p => p.IndexBulkScript));
                using (var response = await HttpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var p = await response.Content.ReadAsStringAsync();
                        return p;
                    }
                    else
                    {
                        throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                e.Data["Param"] = indexEses;
                throw e;
            }
            catch (Exception e)
            {
                e.Data["Param"] = indexEses;
                throw e;
            }
        }

        public async Task<string> Search(EnumDefine.EsIndexName indexName, EnumDefine.EsIndexType indexType, string script)
        {
            var url = $"{(Url.EndsWith("/") ? $"{Url}{indexName}/{indexType}" : $"{Url}/{indexName}/{indexType}")}/_search".ToLower();
            try
            {
                using (var response = await HttpClient.PostAsync(url, script))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var p = await response.Content.ReadAsStringAsync();
                        return p;
                    }
                    else
                    {
                        throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                e.Data["Param"] = new { indexName, indexType, script };
                throw e;
            }
            catch (Exception e)
            {
                e.Data["Param"] = new { indexName, indexType, script };
                throw e;
            }
        }
    }
}
