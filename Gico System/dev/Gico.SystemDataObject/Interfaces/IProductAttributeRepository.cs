using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<RProductAttribute[]> Search(string attributeId, string attributeName, RefSqlPaging paging);
        Task<RProductAttribute> Get(string attributeId);
        Task<RProductAttribute[]> Get(string[] attributeIds);

        #region CRUD

        Task Add(ProductAttribute productAttribute);
        Task<bool> Update(ProductAttribute productAttribute);

        #endregion
    }

    public interface IProductAttributeValueRepository
    {
        Task<RProductAttributeValue[]> Search(string attributeId, string attributeValueId, string value, RefSqlPaging paging);
        Task<RProductAttributeValue> Get(string attributeValueId);
        Task<RProductAttributeValue[]> Get(string[] attributeValueIds);

        #region CRUD

        Task Add(ProductAttributeValue productAttributeValue);
        Task<bool> Update(ProductAttributeValue productAttributeValue);

        #endregion
    }
}