using System.Threading.Tasks;
using Gico.FileCommands;

namespace Gico.FileService.Interfaces
{
    public interface IFileService
    {
        Task ImageAdd(ImageAddCommand command);
    }
}