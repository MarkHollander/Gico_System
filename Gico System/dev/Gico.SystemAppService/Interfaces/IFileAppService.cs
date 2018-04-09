using System.Threading.Tasks;

namespace Gico.SystemAppService.Interfaces
{
    public interface IFileAppService
    {
        Task<string> Upload(string fileName, byte[] bytes);
    }
}