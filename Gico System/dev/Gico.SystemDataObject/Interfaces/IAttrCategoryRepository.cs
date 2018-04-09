using System;
using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.Config;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IAttrCategoryRepository
    {
        Task Add(AttrCategory attrCategory);
        Task<RAttrCategory> Get(int attributeId, string categoryId);
        Task<bool> Change(AttrCategory attrCategory);
        Task Remove(AttrCategory attrCategory);

        Task<RProductAttribute[]> GetsProductAttr(string categoryId);

    }
}
