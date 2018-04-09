using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.Warehouse;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<RWarehouse[]> Search(string code, string email, string phone, string name, EnumDefine.WarehouseStatusEnum status, EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging);

        Task<RWarehouse[]> Search(string venderId, string keyword, EnumDefine.WarehouseStatusEnum status,
            EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging);
        Task<RWarehouse> GetById(string id);
        Task<RWarehouse[]> GetById(string[] ids);

    }
}
