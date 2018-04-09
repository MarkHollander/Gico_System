using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.ReadSystemModels.ProductGroup;
using Gico.SystemCommands.ProductGroup;
using Gico.SystemDomains.ProductGroup;

namespace Gico.SystemService.Interfaces.ProductGroup
{
    public interface IProductGroupService
    {
        #region Read FromDB

        Task<RProductGroup[]> Search(string name, EnumDefine.CommonStatusEnum status, RefSqlPaging sqlPaging);
        Task<RProductGroup> Get(string id);

            #endregion

        #region Change To DB

        Task Add(SystemDomains.ProductGroup.ProductGroup productGroup);
        Task Change(SystemDomains.ProductGroup.ProductGroup productGroup);
        Task ChangeConditions(string id, IDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition> conditions, string updatedUid, DateTime updatedDate);

        #endregion

        #region Send Command

        Task<CommandResult> SendCommand(ProductGroupAddCommand command);
        Task<CommandResult> SendCommand(ProductGroupChangeCommand command);
        Task<CommandResult> SendCommand(ProductGroupCategoryChangeCommand command);
        Task<CommandResult> SendCommand(Command command);
        #endregion
    }
}
