using Gico.EmailOrSmsDomains;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsDataObject.Interfaces
{
    public interface IVerifyRepository
    {
        #region Read

        Task<RVerify> GetById(string id);

        #endregion
        #region Write
        Task Add(Verify verify);
        Task ChangeStatus(string id, EnumDefine.VerifyStatusEnum status);

        #endregion
    }
}