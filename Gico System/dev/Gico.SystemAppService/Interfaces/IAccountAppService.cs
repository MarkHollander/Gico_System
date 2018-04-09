using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IAccountAppService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> CheckLogin();
    }
}