using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request.ProductGroup;
using Gico.SystemModels.Response.ProductGroup;

namespace Gico.SystemAppService.Interfaces.ProductGroup
{
    public interface IProductGroupAppService
    {
        Task<ProductGroupGetsResponse> Gets(ProductGroupGetsRequest request);
        Task<ProductGroupGetResponse> Get(ProductGroupGetRequest request);
        Task<BaseResponse> AddOrChange(ProductGroupAddOrChangeRequest request);
        Task<ProductGroupCategoryGetResponse> GetCategories(ProductGroupCategoryGetRequest request);
        Task<BaseResponse> ChangeCategories(ProductGroupCategoryChangeRequest request);
        Task<ProductGroupVendorGetResponse> SearchVenders(ProductGroupVendorGetRequest request);
        Task<ProductGroupVendorGetResponse> GetVenders(ProductGroupVendorGetRequest request);
        Task<BaseResponse> AddVendor(ProductGroupVendorAddRequest request);
        Task<BaseResponse> RemoveVendor(ProductGroupVendorRemoveRequest request);
        Task<ProductGroupManufacturerGetResponse> SearchManufacturers(ProductGroupManufacturerGetRequest request);
        Task<ProductGroupManufacturerGetResponse> GetManufacturers(ProductGroupManufacturerGetRequest request);
        Task<BaseResponse> AddManufacturer(ProductGroupManufacturerAddRequest request);
        Task<BaseResponse> RemoveManufacturer(ProductGroupManufacturerRemoveRequest request);
        Task<ProductGroupAttributeGetResponse> GetAttributes(ProductGroupAttributeGetRequest request);
        Task<ProductGroupAttributeValueGetResponse> GetAttributeValues(ProductGroupAttributeValueGetRequest request);
        Task<ProductGroupAttributesConfigGetResponse> GetAttributesConfig(ProductGroupAttributesConfigGetRequest request);
        Task<ProductGroupAttributeConfigGetResponse> GetAttributeConfig(ProductGroupAttributeConfigGetRequest request);
        Task<BaseResponse> AddAttributes(ProductGroupAddOrChangeAttributeRequest request);
        Task<BaseResponse> ChangeAttributes(ProductGroupAddOrChangeAttributeRequest request);
        Task<BaseResponse> RemoveAttributes(ProductGroupRemoveAttributeRequest request);
        Task<ProductGroupPriceConfigGetResponse> GetPrices(ProductGroupPriceConfigGetRequest request);
        Task<BaseResponse> ChangePrices(ProductGroupPriceConfigChangeRequest request);
        Task<ProductGroupQuantityConfigGetResponse> GetQuantities(ProductGroupQuantityConfigGetRequest request);
        Task<BaseResponse> ChangeQuantities(ProductGroupQuantityConfigChangeRequest request);

        Task<ProductGroupWarehouseGetResponse> SearchWarehouses(ProductGroupWarehouseGetRequest request);
        Task<ProductGroupWarehouseGetResponse> GetWarehouses(ProductGroupWarehouseGetRequest request);
        Task<BaseResponse> AddWarehouse(ProductGroupWarehouseAddRequest request);
        Task<BaseResponse> RemoveWarehouse(ProductGroupWarehouseRemoveRequest request);

        Task<ProductGroupProductGetResponse> SearchProducts(ProductGroupProductGetRequest request);
        Task<ProductGroupProductGetResponse> GetProducts(ProductGroupProductGetRequest request);
        Task<BaseResponse> AddProduct(ProductGroupProductAddRequest request);
        Task<BaseResponse> RemoveProduct(ProductGroupProductRemoveRequest request);
    }
}
