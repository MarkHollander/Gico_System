using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class LocaleStringResourceCommandHandler : ICommandHandler<LocaleStringResourceAddCommand, ICommandResult>, ICommandHandler<LocaleStringResourceChangeCommand, ICommandResult>
    {
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly ICommonService _commonService;

        public LocaleStringResourceCommandHandler(ILocaleStringResourceService localeStringResourceService, ICommonService commonService)
        {
            _localeStringResourceService = localeStringResourceService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(LocaleStringResourceAddCommand mesage)
        {
            try
            {
                LocaleStringResource locale = new LocaleStringResource();
                locale.Add(mesage);
                await _localeStringResourceService.AddToDb(locale);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = locale.Id,
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



        public async Task<ICommandResult> Handle(LocaleStringResourceChangeCommand mesage)
        {
            try
            {
                var localeStringResourceFromDb = await _localeStringResourceService.GetById(mesage.Id);
                if(localeStringResourceFromDb==null)
                {
                    throw new MessageException(ResourceKey.LocaleStringResource_NotFound);
                }
                LocaleStringResource locale = new LocaleStringResource(localeStringResourceFromDb);
                locale.Change(mesage);

                await _localeStringResourceService.ChangeToDb(locale);


                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = locale.Id,
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
