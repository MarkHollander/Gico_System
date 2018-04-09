using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsDomains;
using Gico.ReadEmailSmsModels;

namespace Gico.EmailOrSmsDataObject.Implements
{
    public class VerifyRepository : SqlBaseDao, IVerifyRepository
    {
        #region Read

        public async Task<RVerify> GetById(string id)
        {
            return await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RVerify>(ProcName.Verify_GetById, parameters, transaction, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        #endregion
        #region Write

        public async Task Add(Verify verify)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", verify.Id, DbType.String);
                parameters.Add("@SaltKey", verify.SaltKey, DbType.String);
                parameters.Add("@SecretKey", verify.SaltKey, DbType.String);
                parameters.Add("@EXPIREDATE", verify.ExpireDate, DbType.DateTime);
                parameters.Add("@TYPE", verify.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@VerifyCode", verify.VerifyCode, DbType.String);
                parameters.Add("@VerifyUrl", verify.VerifyUrl, DbType.String);
                parameters.Add("@Model", Common.Serialize.JsonSerializeObject(verify.Model), DbType.String);
                parameters.Add("@STATUS", verify.Status.AsEnumToInt(), DbType.Int64);
                parameters.Add("@CreatedDateUtc", verify.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedDateUtc", verify.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", verify.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", verify.CreatedUid, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Verify_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task ChangeStatus(string id, EnumDefine.VerifyStatusEnum status)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@STATUS", status.AsEnumToInt(), DbType.Int64);
                var data = await connection.ExecuteAsync(ProcName.Verify_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        #endregion

    }
}
