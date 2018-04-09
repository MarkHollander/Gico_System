using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.Config;

namespace Gico.SystemService.Interfaces
{
    public interface IAttrCategoryService
    {
        Task<CommandResult> SendCommand(AttrCategoryAddCommand command);
        Task<CommandResult> SendCommand(AttrCategoryChangeCommand command);
        Task<CommandResult> SendCommand(AttrCategoryRemoveCommand command);
        Task AddToDb(AttrCategory attrCategory);
        Task ChangeToDb(AttrCategory attrCategory);
        Task RemoveToDb(AttrCategory attrCategory);
        Task<RAttrCategory> Get(int attributeId, string categoryId);

        Task<RProductAttribute[]> GetsProductAttr(string categoryId);

    }
}
