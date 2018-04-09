using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gico.DataObject
{
    public class DbConnectionFactory : IDisposable
    {
        private readonly string _connectionString;
        public DbConnection Connection { get; private set; }

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Open()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection(_connectionString);
                }
                if (Connection.State != ConnectionState.Open)
                {
                    await Connection.OpenAsync();
                }
            }
            catch (Exception e)
            {
                e.Data["DbConnectionFactory.Open"] = "Not new SqlConnection";
                e.Data["DbConnectionFactory.ConnectionString"] = _connectionString;
                throw e;
            }

        }

        public void Dispose()
        {
            try
            {
                if (Connection != null)
                {
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                    Connection.Dispose();
                }
            }
            catch (Exception e)
            {
                e.Data["DbConnectionFactory.Dispose"] = "Not close SqlConnection";
                e.Data["DbConnectionFactory.ConnectionString"] = _connectionString;
                throw e;
            }

        }
    }
}