using System.Threading.Tasks;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEndAppService.Interfaces
{
    public interface ICustomerAppService : IPageAppService
    {
        Task<RegisterViewModel> Register();
        Task<RegisterViewModel> Register(RegisterViewModel model);
        Task<RegisterSuccessViewModel> RegisterSucces();
        Task<LoginViewModel> Login();
        Task<LoginViewModel> Login(LoginViewModel model);
        Task Logout();
        Task<ExternalLoginCallbackViewModel> ExternalLoginCallback();

        Task<SendActiveCodeWhenAccountIsExistModel> SendActiveCodeWhenAccountIsExist();
        Task<ActiveWhenAccountIsExistModel> ActiveWhenAccountIsExist(ActiveWhenAccountIsExistModel model);
        Task<ExternalLoginConfirmViewModel> ExternalLoginConfirm(ExternalLoginConfirmViewModel model);
        Task<VerifyExternalLoginWhenAccountIsExistModel> VerifyExternalLoginWhenAccountIsExist(
           VerifyExternalLoginWhenAccountIsExistModel model);
    }
}