using Dapper;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nop.EventNotify
{
    public class EventLogDao
    {
        private readonly string _connectionString;

        public EventLogDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<DbConnection> GetNewConnection(string connectionString)
        {
            try
            {
                DbConnection dbConnection = new SqlConnection(connectionString);
                await dbConnection.OpenAsync();
                return dbConnection;
            }
            catch (Exception e)
            {
                e.Data["BaseDao.Message-CreateDbConnection"] = "Not new SqlConnection";
                e.Data["BaseDao.ConnectionString"] = connectionString;
                throw e;
            }

        }

        private async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> action)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(this._connectionString))
                {
                    return await action(dbConnection);
                }
            }
            catch (TimeoutException ex)
            {
                ex.Data["EventLogDao.Message-WithConnection"] = "Excute TimeoutException";
                ex.Data["EventLogDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["EventLogDao.Message-WithConnection"] = "Excute SqlException";
                ex.Data["EventLogDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["EventLogDao.Message-WithConnection"] = "Excute Exception";
                ex.Data["EventLogDao.ConnectionString"] = _connectionString;
                throw ex;
            }
        }
        public async Task AddAsync(object eventData, Exception exception)
        {
            await AddAsync(new EventLog()
            {
                Id = 0,
                Data = JsonConvert.SerializeObject(eventData),
                DataType = eventData.GetType().AssemblyQualifiedName,
                Message = JsonConvert.SerializeObject(exception),
                CreatedDateUtc = DateTime.UtcNow,
                Status = -1,
                UpdatedDateUtc = DateTime.UtcNow
            });
        }
        public async Task AddAsync(EventLog eventLog)
        {
            const string spName = "EventLog_Add";
            await WithConnection(async p =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@Data", eventLog.Data, DbType.String);
               parameters.Add("@DataType", eventLog.DataType, DbType.String);
               parameters.Add("@Status", eventLog.Status, DbType.Int32);
               parameters.Add("@CreatedDateUtc", eventLog.CreatedDateUtc, DbType.DateTime);
               parameters.Add("@UpdatedDateUtc", eventLog.CreatedDateUtc, DbType.DateTime);
               parameters.Add("@Message", eventLog.Message, DbType.String);
               return await p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
           });
        }

    }
}