using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Gico.Cdn.WebExtensions;
using Gico.Common;
using Gico.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gico.FileAppService.Interfaces;
using Gico.FileModels.Request;
using Gico.FileModels.Response;
using Gico.Models.Response;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using SixLabors.ImageSharp;

namespace Gico.Cdn.Controllers
{
    [Produces("application/json")]
    [Route("api/images")]
    public class ImagesController : Controller
    {
        private readonly IFileAppService _fileAppService;
        private static readonly FormOptions DefaultFormOptions = new FormOptions();
        public static object LockFile = new object();

        public ImagesController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        public IActionResult Get(string url)
        {
            Uri uri = new Uri(url);
            string filePath = uri.AbsolutePath.Replace("//", "/");
            string path = Path.Combine(Directory.GetCurrentDirectory(), ConfigSettingEnum.UploadPath.GetConfig());
            int lastIndex = filePath.LastIndexOf('/');
            string fileName = filePath.Substring(lastIndex + 1);
            if (Validate(fileName))
            {
                string subPath = filePath.Substring(0, lastIndex);
                var sizeSplit = fileName.Split('_');
                var subPathSplit = subPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string orginPath = path;
                for (int i = 1; i < subPathSplit.Length; i++)
                {
                    orginPath = Path.Combine(orginPath, subPathSplit[i]);
                }
                string fullFilePath = Path.Combine(orginPath, fileName);
                string extentsion = Path.GetExtension(fileName).ToLower().Trim('.');
                if (!System.IO.File.Exists(fullFilePath))
                {
                    int resizeIndex = fileName.IndexOf("resize", StringComparison.OrdinalIgnoreCase);
                    if (resizeIndex >= 0)
                    {
                        int width = sizeSplit[0].AsInt();
                        int height = sizeSplit[1].AsInt();
                        string originFileName = fileName.Substring(resizeIndex + 7);
                        if (ValidateSize(width, height))
                        {
                            string fulloriginFilePath = Path.Combine(orginPath, originFileName);
                            using (Image<Rgba32> image = Image.Load(fulloriginFilePath))
                            {
                                int imgWith = image.Width;
                                int imgHeight = image.Height;
                                if (width + height == 0)
                                {
                                    width = imgWith;
                                    height = imgHeight;
                                }
                                else if (width == 0)
                                {
                                    width = imgWith * height / imgHeight;
                                }
                                else if (height == 0)
                                {
                                    height = imgHeight * width / imgWith;
                                }
                                else
                                {
                                    int newWith = imgWith * height / imgHeight;
                                    if (newWith < width || width == 0)
                                    {
                                        width = newWith;
                                    }
                                    int newHeight = imgHeight * width / imgWith;
                                    if (newHeight < height || height == 0)
                                    {
                                        height = newHeight;
                                    }
                                }
                                string newFile = Path.Combine(orginPath, fileName);
                                image.Mutate(x => x
                                    .Resize(width, height));
                                image.Save(newFile);
                                var stream = new FileStream(newFile, FileMode.Open);
                                return new FileStreamResult(stream, $"image/{extentsion}");
                            }

                        }
                    }
                }
                else
                {
                    var stream = new FileStream(fullFilePath, FileMode.Open);
                    return new FileStreamResult(stream, $"image/{extentsion}");
                }
            }
            return null;
        }

        private bool Validate(string fileName)
        {
            string extentsion = Path.GetExtension(fileName).ToLower().Trim('.');
            if (!ConfigSettingEnum.FilesExtension.GetConfig().Contains(extentsion))
            {
                return false;
            }

            return true;
        }

        private bool ValidateSize(int width, int height)
        {
            if (ConfigSettingEnum.WidthAndHeightAllow.GetConfig().Contains("*"))
            {
                return true;
            }
            string widthAndHeight = $"{width}_{height}";
            return ConfigSettingEnum.WidthAndHeightAllow.GetConfig().Contains(widthAndHeight);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                FileUploadResponse response = new FileUploadResponse();
                DateTime dtNow = DateTime.UtcNow;
                string currentDirectory = Directory.GetCurrentDirectory();
                var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, HttpContext.Request.Body);
                var section = reader.ReadNextSectionAsync().Result;
                while (section != null)
                {
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        var createdUid = HeaderUtilities.RemoveQuotes(contentDisposition.Name).Value;
                        if (string.IsNullOrEmpty(createdUid))
                        {
                            response.SetFail(BaseResponse.ErrorCodeEnum.File_CreatedUserIdIsNullOrEmpty);
                            return Json(response);
                        }
                        var fileNameUpload = HeaderUtilities.RemoveQuotes(contentDisposition.FileName).Value;
                        if (!Validate(fileNameUpload))
                        {
                            response.SetFail(BaseResponse.ErrorCodeEnum.File_FileNameIsNullOrEmpty);
                            return Json(response); 
                        }
                        string fileName = dtNow.ToString("yyyyMMddHHmmss");
                        string filePath = Path.Combine(currentDirectory, ConfigSettingEnum.UploadPath.GetConfig(), createdUid, dtNow.Year.ToString(), dtNow.Month.ToString(), dtNow.Day.ToString());
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string extension = Path.GetExtension(fileNameUpload);
                        string fullFileName = Path.Combine(filePath, string.Format("{0}{1}", fileName, extension));
                        lock (LockFile)
                        {
                            int i = 0;
                            while (System.IO.File.Exists(fullFileName))
                            {
                                i++;
                                fileName += $"_{i}";
                                fullFileName = Path.Combine(filePath, string.Format("{0}{1}", fileName, extension));
                            }

                        }
                        using (var targetStream = System.IO.File.Create(fullFileName))
                        {
                            section.Body.CopyTo(targetStream);
                        }
                        string viewPath = $"{ConfigSettingEnum.CdnPath.GetConfig()}/{createdUid}/{dtNow.Year}/{dtNow.Month}/{dtNow.Day}";
                        response.Status = true;
                        response.Name = string.Format("{0}{1}", fileName, extension);
                        response.Path = viewPath;
                        response.HostName = ConfigSettingEnum.UploadDomain.GetConfig();
                        Console.WriteLine("Response:" + response.FullUrl);
                        await _fileAppService.ImageAdd(new ImageAddModel
                        {
                            FilePath = response.Path,
                            Extension = extension,
                            FileName = response.Name,
                            CreatedUid = createdUid
                        });
                        return Json(response);
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
    }
}