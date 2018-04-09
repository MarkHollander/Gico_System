using System.Threading.Tasks;
using Gico.AppService;
using Gico.SystemAppService.Interfaces;
using Gico.SystemService.Interfaces;

namespace Gico.SystemAppService.Implements
{
    public class FileAppService: IFileAppService
    {
        private readonly IFileService _fileService;
        private readonly ICurrentContext _currentContext;

        public FileAppService(IFileService fileService, ICurrentContext currentContext)
        {
            _fileService = fileService;
            _currentContext = currentContext;
        }

        public async Task<string> Upload( string fileName, byte[] bytes)
        {
            var user = await _currentContext.GetCurrentCustomer();
            string createdUid = user.Id;
            return await _fileService.Upload(createdUid, fileName, bytes);
        }
    }
}