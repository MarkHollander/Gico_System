using Gico.Config;
using Gico.Resilience.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gico.FileStorage
{
    public interface IFileStorage
    {
        Task<string> Upload(string createdUid, string fileName, byte[] bytes);
    }
    public class FileStorage : IFileStorage
    {
        private readonly IHttpClient _httpClient;
        protected static readonly string Url = ConfigSettingEnum.FileStorageUrl.GetConfig();

        public FileStorage(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Upload(string createdUid, string fileName, byte[] bytes)
        {
            try
            {
                var url = (Url.EndsWith("/") ? $"{Url}Images" : $"{Url}/Images").ToLower();
                var fileContent = new ByteArrayContent(bytes);
                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    FileName = fileName,
                    Name = createdUid
                };
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(fileContent);
                    var response = await _httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var p = await response.Content.ReadAsStringAsync();
                        return p;
                    }
                    throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch (HttpRequestException e)
            {
                e.Data["Param"] = new { createdUid, fileName, bytes };
                throw e;
            }
            catch (Exception e)
            {
                e.Data["Param"] = new { createdUid, fileName, bytes };
                throw e;
            }
        }
    }
}
