using Gico.SystemCommands;
using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.CQRS.Model.Implements;
using Gico.SystemService.Interfaces;
using Gico.ReadSystemModels;

namespace Gico.SystemCommandsHandler
{
    public class Category_VariationTheme_MappingHandler: ICommandHandler<Category_VariationTheme_Mapping_AddCommand, ICommandResult>, ICommandHandler<Category_VariationTheme_Mapping_RemoveCommand, ICommandResult>
    {
        private readonly IVariationThemeService _variationThemeService;
        private readonly ICommonService _commonService;
        public Category_VariationTheme_MappingHandler(IVariationThemeService variationThemeService, ICommonService commonService)
        {
            _variationThemeService = variationThemeService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(Category_VariationTheme_Mapping_AddCommand message)
        {
            try
            {
                Category_VariationTheme_Mapping category_VariationTheme_Mapping = new Category_VariationTheme_Mapping();
                category_VariationTheme_Mapping.Add(message);
                await _variationThemeService.AddToDb(category_VariationTheme_Mapping);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = category_VariationTheme_Mapping.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(Category_VariationTheme_Mapping_RemoveCommand message)
        {
            try
            {
                Category_VariationTheme_Mapping category_VariationTheme_Mapping = new Category_VariationTheme_Mapping();
                category_VariationTheme_Mapping.Remove(message);
                await _variationThemeService.RemoveToDb(category_VariationTheme_Mapping);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = category_VariationTheme_Mapping.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

    }
}
