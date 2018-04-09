using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;

namespace Gico.SystemDataObject.Implements
{
    public class CustomerExternalLoginRepository : SqlBaseDao, ICustomerExternalLoginRepository
    {
        public async Task<RCustomerExternalLogin[]> Get(string customerId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId, DbType.String);
                return (await connection.QueryAsync<RCustomerExternalLogin>(ProcName.CustomerExternalLogin_GetByCustomerId, parameters, commandType: CommandType.StoredProcedure)).ToArray();
            });
        }
    }
}
