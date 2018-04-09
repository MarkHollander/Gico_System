using Gico.SystemDomains;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.Config;

namespace Gico.SystemService.Interfaces
{
    public interface ICategoryService
    {
        Task<RCategory[]> Get(string languageId);
        Task<RCategory> Get(string languageId, string id);

        Task<RCategoryAttr[]> GetListAttr(string id, RefSqlPaging sqlPaging);

        Task<RManufacturer[]> GetListManufacturer(string id, RefSqlPaging sqlPaging);

        Task<CommandResult> SendCommand(CategoryAddCommand command);
        Task<CommandResult> SendCommand(CategoryChangeCommand command);
        Task AddToDb(Category category);
        Task ChangeToDb(Category category);

    }
}
