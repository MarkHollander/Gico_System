using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.EmailOrSmsCommands;
using Gico.EmailOrSmsDomains;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsService.Interfaces
{
    public interface IEmailSmsService
    {
        #region Read From DB

        Task<REmailSms> GetFromDb(string id);
        Task<RVerify> GetVerifyFromDb(string id);
        Task<REmailSms[]> Search(EnumDefine.EmailOrSmsTypeEnum type, EnumDefine.EmailOrSmsMessageTypeEnum messageType, string phoneNumber, string email, EnumDefine.EmailOrSmsStatusEnum status, DateTime? createdDateUtc, DateTime? sendDate, RefSqlPaging sqlPaging);
        #endregion

        #region Send command

        Task<CommandResult> SendCommand(EmailOrSmsBaseCommand command);
        Task SendCommand(EmailSmsSendCommand command);

        #endregion

        #region Write To Db
        Task AddToDb(EmailSms emailSms);
        Task ChangeVerifyStatus(string id, EnumDefine.VerifyStatusEnum status);
        Task ChangeToSendSuccess(string id, DateTime sendDate, EnumDefine.EmailOrSmsStatusEnum status, string response);

        #endregion

    }
}