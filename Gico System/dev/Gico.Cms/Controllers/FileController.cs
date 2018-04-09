using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Gico.Cms.WebExtensions;
using Gico.Config;
using Gico.SystemAppService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Gico.Cms.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class FileController : Controller
    {
        private readonly IFileAppService _fileAppService;
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            try
            {
                var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, HttpContext.Request.Body);
                var section = reader.ReadNextSectionAsync().Result;
                while (section != null)
                {
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        var fileNameUpload = HeaderUtilities.RemoveQuotes(contentDisposition.FileName).Value;
                        var bytes = StreamToBytes(section.Body);
                        var result = await _fileAppService.Upload(fileNameUpload, bytes);
                        return Json(result);
                    }
                    section = await reader.ReadNextSectionAsync();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}