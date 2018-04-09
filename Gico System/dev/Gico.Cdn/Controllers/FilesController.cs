using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gico.Common;
using Gico.Config;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace Gico.Cdn.Controllers
{
    [Route("files")]
    public class FilesController : Controller
    {
        [Route("{*url:regex(^(?!files).*$)}")]
        public IActionResult Index()
        {
            Uri uri = Request.GetUri();
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
            return NotFound();
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
    }
}