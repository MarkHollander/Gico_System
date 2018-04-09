using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;

namespace Gico.DataObject
{
    public abstract class BaseDao : IBaseDao
    {
        public abstract string ConnectionString { get; }

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

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(this.ConnectionString))
                {
                    return await getData(dbConnection);
                }
            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute SqlException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, IDbTransaction, Task<T>> getData)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(this.ConnectionString))
                {
                    using (IDbTransaction transaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            var result = await getData(dbConnection, transaction);
                            transaction.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                            ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                            throw;
                        }
                    }
                }

            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction SqlException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }

        }
        protected async Task<T> WithConnection<T>(string connectionString, Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(connectionString))
                {
                    return await getData(dbConnection);
                }
            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute SqlException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
        }

        protected async Task<T> WithConnection<T>(string connectionString, Func<IDbConnection, IDbTransaction, Task<T>> getData)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(connectionString))
                {
                    using (IDbTransaction transaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            var result = await getData(dbConnection, transaction);
                            transaction.Commit();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                            ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                            throw;
                        }
                    }
                }

            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction SqlException";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw ex;
            }

        }
        protected async Task BulkCopy(DataTable table)
        {
            try
            {
                using (var dbConnection = await GetNewConnection(this.ConnectionString))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)dbConnection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.UseInternalTransaction, null)
                    {
                        DestinationTableName = table.TableName,
                        BatchSize = 1000,
                    };
                    foreach (DataColumn tableColumn in table.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(tableColumn.ColumnName, tableColumn.ColumnName);
                    }
                    await bulkCopy.WriteToServerAsync(table);
                    table.Clear();
                }

            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-BulkCopy"] = "BulkCopy Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw;
            }
        }
        protected async Task BulkCopy(DataTable table, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.TableLock, (SqlTransaction)transaction)
                {
                    DestinationTableName = table.TableName,
                    BatchSize = 1000
                };
                foreach (DataColumn tableColumn in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(tableColumn.ColumnName, tableColumn.ColumnName);
                }
                await bulkCopy.WriteToServerAsync(table);
                table.Clear();


            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-BulkCopy"] = "BulkCopy Exception";
                ex.Data["BaseDao.ConnectionString"] = ConnectionString;
                throw;
            }
        }

    }
}
