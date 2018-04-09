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
    public class AttrCategoryCommandHandler : ICommandHandler<AttrCategoryAddCommand, ICommandResult>, ICommandHandler<AttrCategoryChangeCommand, ICommandResult>, ICommandHandler<AttrCategoryRemoveCommand, ICommandResult>
    {
        private readonly IAttrCategoryService _attrCategoryService;
        private readonly ICommonService _commonService;
        public AttrCategoryCommandHandler(IAttrCategoryService attrCategoryService, ICommonService commonService)
        {
            _attrCategoryService = attrCategoryService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(AttrCategoryAddCommand mesage)
        {
            try
            {
                AttrCategory attrCategory = new AttrCategory();
                attrCategory.Add(mesage);
                await _attrCategoryService.AddToDb(attrCategory);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = attrCategory.Id,
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


        public async Task<ICommandResult> Handle(AttrCategoryChangeCommand mesage)
        {
            try
            {
                AttrCategory attrCategory = new AttrCategory();
                attrCategory.Change(mesage);
                await _attrCategoryService.ChangeToDb(attrCategory);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = attrCategory.Id,
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

        public async Task<ICommandResult> Handle(AttrCategoryRemoveCommand mesage)
        {
            try
            {
                AttrCategory attrCategory = new AttrCategory();
                attrCategory.Remove(mesage);
                await _attrCategoryService.RemoveToDb(attrCategory);

                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = attrCategory.Id,
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
