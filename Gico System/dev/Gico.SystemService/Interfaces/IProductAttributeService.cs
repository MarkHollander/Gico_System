using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;

namespace Gico.SystemService.Interfaces
{
    public interface IProductAttributeService
    {
        #region Get

        Task<RProductAttribute[]> Search(string attributeId, string attributeName, RefSqlPaging paging);
        Task<RProductAttribute> GetFromDb(string attributeId);
        Task<RProductAttribute[]> GetFromDb(string[] attributeIds);

        #endregion

        #region Command

        Task<CommandResult> SendCommand(ProductAttributeCommand command);

        #endregion

        #region CRUD

        Task AddToDb(ProductAttribute productAttribute);
        Task UpdateToDb(ProductAttribute productAttribute);

        #endregion
    }

    public interface IProductAttributeValueService
    {
        #region Get

        Task<RProductAttributeValue[]> Search(string attributeId, string attributeValueId, string value, RefSqlPaging paging);
        Task<RProductAttributeValue> GetFromDb(string attributeValueId);
        Task<RProductAttributeValue[]> GetFromDb(string[] attributeValueIds);

        #endregion

        #region Command

        Task<CommandResult> SendCommand(ProductAttributeValueCommand command);

        #endregion

        #region CRUD

        Task AddToDb(ProductAttributeValue productAttributeValue);
        Task UpdateToDb(ProductAttributeValue productAttributeValue);

        #endregion
    }
}