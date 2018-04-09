using System.Threading.Tasks;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEndAppService.Interfaces
{
    public interface ICheckoutAppService
    {
        Task<CheckoutViewModel> Checkout();
        Task<LocationViewModel[]> LocationSearch();
        Task<CheckoutViewModel> AddressSelected( AddressSelectedViewModel model);
    }
}