using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.EmailOrSmsCommands;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsDomains;
using Gico.EmailOrSmsService.Interfaces;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsService.Implements
{
    public class EmailSmsService : IEmailSmsService
    {
        private readonly ICommandSender _commandService;
        private readonly IEmailSmsRepository _emailSmsRepository;
        private readonly IVerifyRepository _verifyRepository;
        public EmailSmsService(ICommandSender commandService, IEmailSmsRepository emailSmsRepository, IVerifyRepository verifyRepository)
        {
            _commandService = commandService;
            _emailSmsRepository = emailSmsRepository;
            _verifyRepository = verifyRepository;
        }

        #region Read FromDb

        public async Task<REmailSms> GetFromDb(string id)
        {
            REmailSms emailSms = await _emailSmsRepository.Get(id);
            if (!string.IsNullOrEmpty(emailSms.VerifyId))
            {
                emailSms.Verify = await GetVerifyFromDb(emailSms.VerifyId);
            }
            return emailSms;
        }
        public async Task<RVerify> GetVerifyFromDb(string id)
        {
            return await _verifyRepository.GetById(id);
        }

        public async Task<REmailSms[]> Search(EnumDefine.EmailOrSmsTypeEnum type, EnumDefine.EmailOrSmsMessageTypeEnum messageType, string phoneNumber, string email, EnumDefine.EmailOrSmsStatusEnum status, DateTime? createdDateUtc, DateTime? sendDate, RefSqlPaging sqlPaging)
        {
            return await _emailSmsRepository.Search(type, messageType, phoneNumber, email, status, createdDateUtc, sendDate, sqlPaging);
        }

        #endregion

        #region Send Command

        public async Task<CommandResult> SendCommand(EmailOrSmsBaseCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task SendCommand(EmailSmsSendCommand command)
        {
            await _commandService.Send(command, false);
        }

        #endregion

        #region Write To Db

        public async Task AddToDb(EmailSms emailSms)
        {
            await _emailSmsRepository.Add(emailSms);
        }

        public async Task ChangeVerifyStatus(string id, EnumDefine.VerifyStatusEnum status)
        {
            await _verifyRepository.ChangeStatus(id, status);
        }

        public async Task ChangeToSendSuccess(string id, DateTime sendDate, EnumDefine.EmailOrSmsStatusEnum status, string response)
        {
            await _emailSmsRepository.ChangeToSendSuccess(id, sendDate, status, response);
        }

        #endregion

    }
}
