using Gico.Config;
using Gico.DataObject;

namespace Gico.EmailOrSmsDataObject
{
    public class SqlBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbEmailOrSmsConnectionString.GetConfig();
        
        public class ProcName
        {
            #region Verify

            public const string Verify_Add = "Verify_Add";
            public const string Verify_GetById = "Verify_GetById";
            public const string Verify_ChangeStatus = "Verify_ChangeStatus";

            #endregion

            #region EmailSms
            public const string EmailSms_Add = "EmailSms_Add";
            public const string EmailSms_GetById = "EmailSms_GetById";
            public const string EmailSms_SendSuccess = "EmailSms_SendSuccess";
            public const string EmailSms_Search = "EmailSms_Search";

            #endregion

        }
    }
}
