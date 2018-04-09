using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels.Warehouse;
using Gico.SystemCacheStorage.Interfaces.Product;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemService.Interfaces.Product;
using Gico.SystemService.Interfaces.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.SystemService.Implements.Warehouse
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ICommandSender _commandService;
        private readonly IWarehouseRepository _warehouseRepository;


        public WarehouseService(ICommandSender commandService, IWarehouseRepository warehouseRepository)
        {
            _commandService = commandService;
            _warehouseRepository = warehouseRepository;

        }

        public async Task<RWarehouse[]> Search(string venderId, string keyword, EnumDefine.WarehouseStatusEnum status, EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging)
        {
            return await _warehouseRepository.Search(venderId, keyword, status, type, paging);
        }

        public async Task<RWarehouse> GetById(string id)
        {
            return await _warehouseRepository.GetById(id);
        }

        public async Task<RWarehouse[]> GetById(string[] ids)
        {
            return await _warehouseRepository.GetById(ids);
        }

        public async Task<RWarehouse[]> Search(string code, string email, string phone, string name, EnumDefine.WarehouseStatusEnum status, EnumDefine.WarehouseTypeEnum type, RefSqlPaging paging)
        {
            return await _warehouseRepository.Search(code, email, phone, name, status, type, paging);
        }
    }
}
