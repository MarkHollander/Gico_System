using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels.Warehouse;
using Gico.SystemCommands;
using Gico.SystemDomains;
namespace Gico.SystemService.Interfaces.Warehouse
{
    public interface IWarehouseService
    {
        #region READ
        Task<RWarehouse[]> Search(string code, string email, string phone, string name, EnumDefine.WarehouseStatusEnum status,EnumDefine.WarehouseTypeEnum type , RefSqlPaging paging);

        Task<RWarehouse[]> Search(string venderId, string keyword, EnumDefine.WarehouseStatusEnum status,
            EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging);
        Task<RWarehouse> GetById(string id);
        Task<RWarehouse[]> GetById(string[] ids);

        #endregion

    }
}
