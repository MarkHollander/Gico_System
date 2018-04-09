using System.Threading.Tasks;

namespace Gico.SystemService.Interfaces
{
    public interface IFileService
    {
        Task<string> Upload(string createdUid, string fileName, byte[] bytes);
    }
}