using Gico.CQRS.Service.Interfaces;
using Gico.SystemCacheStorage.Interfaces.Product;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemService.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.Product;

namespace Gico.SystemService.Implements.Product
{
    public class ProductService : IProductService
    {
        private readonly ICommandSender _commandService;
        private readonly IProductRepository _productRepository;
        private IProductCacheStorage _productCacheStorage;

        public ProductService(ICommandSender commandService, IProductRepository productRepository, IProductCacheStorage productCacheStorage)
        {
            this._commandService = commandService;
            this._productRepository = productRepository;
            this._productCacheStorage = productCacheStorage;
        }

        public async Task<RProduct[]> GetById(string[] ids)
        {
            if (ids == null || ids.Length <= 0)
            {
                return new RProduct[0];
            }
            return await _productRepository.GetById(ids);
        }

        public async Task<RProduct[]> SearchByCodeAndName(string keyword, EnumDefine.ProductStatus status, RefSqlPaging sqlPaging)
        {
            return await _productRepository.SearchByCodeAndName(keyword, status, sqlPaging);
        }
    }
}
