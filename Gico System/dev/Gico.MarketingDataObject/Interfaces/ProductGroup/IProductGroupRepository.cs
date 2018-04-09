using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels.ProductGroup;

namespace Gico.MarketingDataObject.Interfaces.ProductGroup
{
    public interface IProductGroupRepository
    {
        Task<RProductGroup[]> Search(string name, EnumDefine.CommonStatusEnum status, RefSqlPaging sqlPaging);
        Task<RProductGroup> Get(string id);
        Task Add(SystemDomains.ProductGroup.ProductGroup productGroup);
        Task Change(SystemDomains.ProductGroup.ProductGroup productGroup);
        Task ChangeConditions(string id, string conditions, string updatedUid, DateTime updatedDate);
    }
}
