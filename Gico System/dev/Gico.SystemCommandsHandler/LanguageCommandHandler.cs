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
    public class LanguageCommandHandler : ICommandHandler<LanguageAddCommand, ICommandResult>, ICommandHandler<LanguageChangeCommand, ICommandResult>
    {
        private readonly ILanguageService _languageService;
        private readonly ICommonService _commonService;
        public LanguageCommandHandler(ILanguageService languageService, ICommonService commonService)
        {
            _languageService = languageService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(LanguageAddCommand mesage)
        {
            try
            {
                Language language = new Language();
                language.Add(mesage);
                await _languageService.AddToDb(language);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = language.Id,
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

        public async Task<ICommandResult> Handle(LanguageChangeCommand mesage)
        {
            try
            {
                RLanguage languageFromDb = await _languageService.GetFromDb(mesage.Id.ToString());
                if (languageFromDb == null)
                {
                    throw new MessageException(ResourceKey.Language_NotFound);
                }
                Language language = new Language();
                language.Init(languageFromDb);

                language.Change(mesage);
                await _languageService.ChangeToDb(language);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = language.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail,
                    ResourceName = e.ResourceName
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