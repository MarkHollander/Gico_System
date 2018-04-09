using Gico.CQRS.Service.Interfaces;
using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.EmailOrSmsCommands;
using Gico.ExceptionDefine;
using Gico.EmailOrSmsDomains;
using Gico.EmailOrSmsService.Interfaces;
using Gico.Razor;
using Gico.ReadEmailSmsModels;
using Gico.SendMail;
using Gico.EmailSmsTemplateModels;

namespace Gico.EmailOrSmsCommandsHandler
{
    public class EmailOrSmsCommandHandler : ICommandHandler<EmailOrSmsBaseCommand, ICommandResult>, ICommandHandler<EmailSmsSendCommand, ICommandResult>
    {
        private readonly IEmailSmsService _emailSmsService;
        private readonly ISendMailClient _sendMailClient;
        private readonly ITemplateRenderer _templateRenderer;
        public EmailOrSmsCommandHandler(IEmailSmsService emailSmsService, ISendMailClient sendMailClient, ITemplateRenderer templateRenderer)
        {
            _emailSmsService = emailSmsService;
            _sendMailClient = sendMailClient;
            _templateRenderer = templateRenderer;
        }

        public async Task<ICommandResult> Handle(EmailOrSmsBaseCommand command)
        {
            try
            {
                EmailSms emailOrSms = new EmailSms();
                string file = command.Type.GetDisplayName();
                object model = null;
                switch (command.Type)
                {
                    case EnumDefine.EmailOrSmsTypeEnum.ExternalLoginConfirmWhenAccountIsExist:
                        model = Common.Serialize.JsonDeserializeObject<TestEmailSmsModel>(command.Model.ToString());
                        break;
                }
                string content = await _templateRenderer.ParseFromTemplateFileAsync(file, model);
                emailOrSms.Create(command, content);
                await _emailSmsService.AddToDb(emailOrSms);
                await _emailSmsService.SendCommand(new EmailSmsSendCommand()
                {
                    Id = emailOrSms.Id
                });
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = emailOrSms.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (MessageException e)
            {
                e.Data["Param"] = command;
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
                e.Data["Param"] = command;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }


        public async Task<ICommandResult> Handle(EmailSmsSendCommand mesage)
        {
            try
            {
                if (string.IsNullOrEmpty(mesage.Id))
                {
                    throw new MessageException(ResourceKey.EmailSms_EmailSmsSendCommand_IsNotNullOrEmpty);
                }
                REmailSms emailSms = await _emailSmsService.GetFromDb(mesage.Id);
                if (emailSms == null)
                {
                    throw new MessageException(ResourceKey.EmailSms_NotFound);
                }
                EmailSms emailOrSms = new EmailSms(emailSms);
                Ref<string> response = new Ref<string>();
                bool isOk = false;
                if (emailOrSms.CheckMessageType(EnumDefine.EmailOrSmsMessageTypeEnum.Email))
                {
                    isOk = await _sendMailClient.Send(ConfigSettingEnum.AwsSenderAddress.GetConfig(), emailOrSms.Email, emailOrSms.Title, emailOrSms.Content, response);
                }
                if (emailOrSms.CheckMessageType(EnumDefine.EmailOrSmsMessageTypeEnum.Sms))
                {
                    //await _sendMailClient.Send(ConfigSettingEnum.AwsSenderAddress.GetConfig(), emailOrSms.Email, emailOrSms.Title, emailOrSms.Content);
                }
                if (isOk)
                {
                    emailOrSms.SetSendSuccess();
                }
                else
                {
                    emailOrSms.SetSendFail();
                }
                await _emailSmsService.ChangeToSendSuccess(emailOrSms.Id, emailOrSms.SendDate.GetValueOrDefault(), emailOrSms.Status, response.Value);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = emailOrSms.Id,
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
