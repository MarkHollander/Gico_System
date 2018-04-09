using System.Threading.Tasks;
using Gico.FileStorage;
using Gico.SystemService.Interfaces;

namespace Gico.SystemService.Implements
{
    public class FileService: IFileService
    {
        private readonly IFileStorage _fileStorage;

        public FileService(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<string> Upload(string createdUid, string fileName, byte[] bytes)
        {
            return await _fileStorage.Upload(createdUid, fileName, bytes);
        }
    }
}