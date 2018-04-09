using System;
using System.IO;
using System.Threading.Tasks;
using RazorLight;

namespace Gico.Razor
{
    public interface ITemplateRenderer
    {
        Task<string> ParseAsync<T>(string template, T model, bool isHtml = true);
        Task<string> ParseFromTemplateFileAsync<T>(string template, T model, bool isHtml = true);
    }
    public class RazorRenderer : ITemplateRenderer
    {
        public async Task<string> ParseAsync<T>(string template, T model, bool isHtml = true)
        {
            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
            string result = await engine.CompileRenderAsync(Guid.NewGuid().ToString(), template, model);
            return result;
        }
        public async Task<string> ParseFromTemplateFileAsync<T>(string template, T model, bool isHtml = true)
        {
            string path = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(path, "EmailSmsTemplates", template);
            string tmp = File.ReadAllText(fullPath);
            return await ParseAsync(tmp, model, isHtml);

        }
    }

}
