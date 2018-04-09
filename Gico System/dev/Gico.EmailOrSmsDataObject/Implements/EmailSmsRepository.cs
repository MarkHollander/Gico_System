using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsDomains;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsDataObject.Implements
{
    public class EmailSmsRepository : SqlBaseDao, IEmailSmsRepository
    {
        #region Read

        public async Task<REmailSms> Get(string id)
        {
            return await WithConnection(async (connection, transaction) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@Id", id, DbType.String);
                 var data = await connection.QueryFirstOrDefaultAsync<REmailSms>(ProcName.EmailSms_GetById, parameters, transaction, commandType: CommandType.StoredProcedure);
                 return data;
             });
        }

        public async Task<REmailSms[]> Search(EnumDefine.EmailOrSmsTypeEnum type, EnumDefine.EmailOrSmsMessageTypeEnum messageType, string phoneNumber, string email, EnumDefine.EmailOrSmsStatusEnum status, DateTime? createdDateUtc, DateTime? sendDate, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Type", type, DbType.Int32);
                parameters.Add("@MessageType", messageType, DbType.Int32);
                parameters.Add("@PhoneNumber", phoneNumber, DbType.String);
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@CreatedDateUtc", createdDateUtc, DbType.DateTime);
                parameters.Add("@SendDate", sendDate, DbType.DateTime);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<REmailSms>(ProcName.EmailSms_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }

        #endregion
        #region Write

        public async Task Add(EmailSms emailSms)
        {
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", emailSms.Id, DbType.String);
                parameters.Add("@TYPE", emailSms.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@MessageType", emailSms.MessageType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@PhoneNumber", emailSms.PhoneNumber, DbType.String);
                parameters.Add("@Email", emailSms.Email, DbType.String);
                parameters.Add("@Title", emailSms.Title, DbType.String);
                parameters.Add("@CONTENT", emailSms.Content, DbType.String);
                parameters.Add("@Model", Serialize.JsonSerializeObject(emailSms.Model) ?? string.Empty, DbType.String);
                parameters.Add("@Template", emailSms.Template ?? string.Empty, DbType.String);
                parameters.Add("@STATUS", emailSms.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@CreatedDateUtc", emailSms.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", emailSms.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", emailSms.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", emailSms.CreatedUid, DbType.String);
                parameters.Add("@VerifyId", emailSms.VerifyId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.EmailSms_Add, parameters, transaction, commandType: CommandType.StoredProcedure);
                if (data > 0 && emailSms.Verify != null)
                {
                    await Add(emailSms.Verify, connection, transaction);
                }
                return data;
            });
        }
        private async Task Add(Verify verify, IDbConnection connection, IDbTransaction transaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", verify.Id, DbType.String);
            parameters.Add("@SaltKey", verify.SaltKey, DbType.String);
            parameters.Add("@SecretKey", verify.SecretKey, DbType.String);
            parameters.Add("@EXPIREDATE", verify.ExpireDate, DbType.DateTime);
            parameters.Add("@TYPE", verify.Type.AsEnumToInt(), DbType.Int32);
            parameters.Add("@VerifyCode", verify.VerifyCode, DbType.String);
            parameters.Add("@VerifyUrl", verify.VerifyUrl, DbType.String);
            parameters.Add("@Model", Common.Serialize.JsonSerializeObject(verify.Model), DbType.String);
            parameters.Add("@STATUS", verify.Status.AsEnumToInt(), DbType.Int32);
            parameters.Add("@CreatedDateUtc", verify.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@UpdatedDateUtc", verify.CreatedDateUtc, DbType.DateTime);
            parameters.Add("@CreatedUid", verify.CreatedUid, DbType.String);
            parameters.Add("@UpdatedUid", verify.CreatedUid, DbType.String);
            var data = await connection.ExecuteAsync(SqlBaseDao.ProcName.Verify_Add, parameters, transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task ChangeToSendSuccess(string id, DateTime sendDate, EnumDefine.EmailOrSmsStatusEnum status, string response)
        {
            await WithConnection(async (connection, transaction) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Id", id, DbType.String);
               parameters.Add("@SendDate", sendDate, DbType.DateTime);
               parameters.Add("@Status", status.AsEnumToInt(), DbType.Int64);
               parameters.Add("@Response", response, DbType.String);
               var data = await connection.ExecuteAsync(ProcName.EmailSms_SendSuccess, parameters, transaction, commandType: CommandType.StoredProcedure);
               return data;
           });
        }
        #endregion

    }
}
