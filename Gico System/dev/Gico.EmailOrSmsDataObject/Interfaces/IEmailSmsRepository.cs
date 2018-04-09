using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.EmailOrSmsDomains;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsDataObject.Interfaces
{
    public interface IEmailSmsRepository
    {
        #region Read

        Task<REmailSms> Get(string id);
        Task<REmailSms[]> Search(EnumDefine.EmailOrSmsTypeEnum type, EnumDefine.EmailOrSmsMessageTypeEnum messageType, string phoneNumber, string email, EnumDefine.EmailOrSmsStatusEnum status, DateTime? createdDateUtc, DateTime? sendDate, RefSqlPaging sqlPaging);

        #endregion
        #region Write
        Task Add(EmailSms emailSms);
        Task ChangeToSendSuccess(string id, DateTime sendDate, EnumDefine.EmailOrSmsStatusEnum status, string response);

        #endregion
    }
}