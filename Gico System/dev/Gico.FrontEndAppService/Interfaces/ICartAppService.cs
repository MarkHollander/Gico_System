using System.Threading.Tasks;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEndAppService.Interfaces
{
    public interface ICartAppService: IPageAppService
    {
        Task<CartViewModel> Get();
        Task<CartViewModel> Change( CartItemChangeViewModel item);
    }
}