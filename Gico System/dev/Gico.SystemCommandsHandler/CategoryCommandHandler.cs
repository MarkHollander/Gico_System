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
    public class CategoryCommandHandler:ICommandHandler<CategoryAddCommand, ICommandResult>, ICommandHandler<CategoryChangeCommand, ICommandResult>
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommonService _commonService;
        public CategoryCommandHandler(ICategoryService categoryService, ICommonService commonService)
        {
            _categoryService = categoryService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(CategoryAddCommand mesage)
        {
            try
            {
                Category category = new Category();
                category.Add(mesage);
                await _categoryService.AddToDb(category);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = category.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(CategoryChangeCommand mesage)
        {
            try
            {
                Category category = new Category();
                RCategory categoryFromDb = await _categoryService.Get(mesage.LanguageId,mesage.Id);
                //string code = string.Empty;   
                //if (string.IsNullOrEmpty(categoryFromDb.Code))
                //{
                //    long systemIdentity = await _commonService.GetNextId(typeof(Customer));
                //    code = Common.Common.GenerateCodeFromId(systemIdentity, 3);
                //}
                category.Change(mesage);
                await _categoryService.ChangeToDb(category);
                //await _categoryRepository.Change(category);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = category.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
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
