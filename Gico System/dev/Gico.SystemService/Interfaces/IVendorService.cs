
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface IVendorService
    {
        #region READ From DB
        Task<RVendor[]> Search(string code, string email, string phone, string name, EnumDefine.VendorStatusEnum status, RefSqlPaging paging);
        Task<RVendor[]> Search(string keyword, EnumDefine.VendorStatusEnum status, RefSqlPaging paging);
        Task<RVendor> GetFromDb(string id);
        Task<RVendor[]> GetFromDb(string[] ids);
        #endregion

        #region Write To Db
        Task AddToDb(Vendor vendor);
        Task ChangeToDb(Vendor vendor, string code);


        #endregion

        #region Command

        Task<CommandResult> SendCommand(VendorAddCommand command);
        Task<CommandResult> SendCommand(VendorChangeCommand command);

        #endregion

        #region Get From Cache

        Task<RCustomer> GetLoginInfoFromCache(string key);

        #endregion
    }
}