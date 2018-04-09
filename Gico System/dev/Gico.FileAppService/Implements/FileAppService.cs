using System;
using System.Threading.Tasks;
using Gico.FileAppService.Interfaces;
using Gico.FileAppService.Mapping;
using Gico.FileModels.Request;
using Gico.FileService.Interfaces;
using Microsoft.Extensions.Logging;
using Gico.FileModels.Response;

namespace Gico.FileAppService.Implements
{
    public class FileAppService : IFileAppService
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileAppService> _logger;

        public FileAppService(IFileService fileService, ILogger<FileAppService> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<ImageAddResponse> ImageAdd(ImageAddModel model)
        {
            ImageAddResponse response = new ImageAddResponse();
            try
            {
                var command = model.ToCommand();
                await _fileService.ImageAdd(command);
                response.SetSucess();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, model);
            }
            response.SetSucess();
            return response;
        }
    }
}
