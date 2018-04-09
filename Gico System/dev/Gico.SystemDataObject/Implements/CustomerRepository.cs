using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class CustomerRepository : SqlBaseDao, ICustomerRepository
    {

        #region read 
        public async Task<RCustomer> Get(string id)
        {
            return await WithConnection(async (connection) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@Id", id, DbType.String);
                 return await connection.QueryFirstOrDefaultAsync<RCustomer>(ProcName.Customer_GetById, parameters, commandType: CommandType.StoredProcedure);
             });
        }

        public async Task<RCustomer> GetByEmailOrPhone(string emailOrMobile)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@emailOrMobile", emailOrMobile, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RCustomer>(ProcName.Customer_GetByEmailOrMobile, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RCustomer[]> Search(string code, string email, string mobile, string fullName, DateTime? fromBirthday, DateTime? toBirthday, EnumDefine.CustomerTypeEnum type, EnumDefine.CustomerStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", code, DbType.String);
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@PhoneNumber", mobile, DbType.String);
                parameters.Add("@FullName", fullName, DbType.String);
                parameters.Add("@FromBirthday", fromBirthday, DbType.String);
                parameters.Add("@ToBirthday", toBirthday, DbType.String);
                parameters.Add("@Type", type.AsEnumToInt(), DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.String);
                parameters.Add("@OFFSET", paging.OffSet, DbType.String);
                parameters.Add("@FETCH", paging.PageSize, DbType.String);
                var data = await connection.QueryAsync<RCustomer>(ProcName.Customer_Search, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        #endregion

        #region write
        public async Task Add(Customer customer)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.Id, DbType.String);
                parameters.Add("@CustomerGuid", null, DbType.String);
                parameters.Add("@Email", customer.Email, DbType.String);
                parameters.Add("@EmailToRevalidate", customer.EmailToRevalidate, DbType.String);
                parameters.Add("@AdminComment", customer.AdminComment, DbType.String);
                parameters.Add("@IsTaxExempt", customer.IsTaxExempt, DbType.Boolean);
                parameters.Add("@LastIpAddress", customer.LastIpAddress, DbType.String);
                parameters.Add("@BillingAddressId", customer.BillingAddressId, DbType.String);
                parameters.Add("@ShippingAddressId", customer.BillingAddressId, DbType.String);
                parameters.Add("@Code", customer.Code, DbType.String);
                parameters.Add("@PASSWORD", customer.Password, DbType.String);
                parameters.Add("@PasswordFormatId", null, DbType.Int32);
                parameters.Add("@PasswordSalt", customer.PasswordSalt, DbType.String);
                parameters.Add("@PhoneNumber", customer.PhoneNumber, DbType.String);
                parameters.Add("@PhoneNumberConfirmed", customer.PhoneNumberConfirmed, DbType.Boolean);
                parameters.Add("@TwoFactorEnabled", customer.TwoFactorEnabled.AsEnumToInt(), DbType.Int32);
                parameters.Add("@FullName", customer.FullName, DbType.String);
                parameters.Add("@Gender", customer.Gender.AsEnumToInt(), DbType.Int32);
                parameters.Add("@Birthday", customer.Birthday, DbType.DateTime);
                parameters.Add("@TYPE", customer.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@STATUS", customer.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@EmailConfirmed", customer.EmailConfirmed, DbType.String);
                parameters.Add("@CreatedDateUtc", customer.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@LanguageId", customer.LanguageId, DbType.String);
                parameters.Add("@UpdatedDateUtc", customer.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", customer.CreatedUid, DbType.String);
                parameters.Add("@UpdatedUid", customer.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", customer.Version, DbType.Int32);
                return await connection.ExecuteAsync(ProcName.Customer_Add, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<bool> Change(Customer customer, string code)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.Id, DbType.String);
                if (!string.IsNullOrEmpty(code))
                {
                    parameters.Add("@Code", code, DbType.String);
                }
                parameters.Add("@AdminComment", customer.AdminComment, DbType.String);
                parameters.Add("@IsTaxExempt", customer.IsTaxExempt, DbType.Boolean);
                parameters.Add("@LastIpAddress", customer.LastIpAddress, DbType.String);
                parameters.Add("@BillingAddressId", customer.BillingAddressId, DbType.String);
                parameters.Add("@ShippingAddressId", customer.BillingAddressId, DbType.String);
                parameters.Add("@TwoFactorEnabled", customer.TwoFactorEnabled.AsEnumToInt(), DbType.Int32);
                parameters.Add("@FullName", customer.FullName, DbType.String);
                parameters.Add("@Gender", customer.Gender.AsEnumToInt(), DbType.Int32);
                parameters.Add("@Birthday", customer.Birthday, DbType.DateTime);
                parameters.Add("@TYPE", customer.Type.AsEnumToInt(), DbType.Int32);
                parameters.Add("@STATUS", customer.Status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@LanguageId", customer.LanguageId, DbType.String);
                parameters.Add("@UpdatedDateUtc", customer.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", customer.UpdatedUid, DbType.String);
                parameters.Add("@VERSION", customer.Version, DbType.Int32);
                var rowCount = 0;
                if (!string.IsNullOrEmpty(code))
                {
                    rowCount = await connection.ExecuteAsync(ProcName.Customer_ChangeAndChangeCode, parameters, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    rowCount = await connection.ExecuteAsync(ProcName.Customer_Change, parameters, commandType: CommandType.StoredProcedure);
                }
                return rowCount == 1;
            });
        }

        public async Task<bool> Change(Customer customer, CustomerExternalLogin customerExternalLogin)
        {
            return await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.Id, DbType.String);
                parameters.Add("@VERSION", customer.Version, DbType.Int32);
                int rowCount = await connection.ExecuteAsync(ProcName.Customer_ChangeVersion, parameters, transaction, commandType: CommandType.StoredProcedure);
                if (rowCount == 0)
                {
                    throw new MessageException(ResourceKey.Customer_Version_Changed);
                }
                parameters = new DynamicParameters();
                parameters.Add("@LoginProvider", customerExternalLogin.LoginProvider.AsEnumToInt(), DbType.Int32);
                parameters.Add("@ProviderKey", customerExternalLogin.ProviderKey, DbType.String);
                parameters.Add("@ProviderDisplayName", customerExternalLogin.ProviderDisplayName, DbType.String);
                parameters.Add("@CustomerId", customerExternalLogin.CustomerId, DbType.String);
                parameters.Add("@Info", customerExternalLogin.Info, DbType.String);
                await connection.ExecuteAsync(ProcName.CustomerExternalLogin_Add, parameters, transaction, commandType: CommandType.StoredProcedure);
                return true;
            });
        }
        #endregion
    }
}