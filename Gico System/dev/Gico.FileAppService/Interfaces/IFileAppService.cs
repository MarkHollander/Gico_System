using System.Threading.Tasks;
using Gico.FileModels.Request;
using Gico.FileModels.Response;

namespace Gico.FileAppService.Interfaces
{
    public interface IFileAppService
    {
        Task<ImageAddResponse> ImageAdd(ImageAddModel model);
    }
}