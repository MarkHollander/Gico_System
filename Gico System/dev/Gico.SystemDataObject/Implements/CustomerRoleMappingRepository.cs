using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class CustomerRoleMappingRepository : SqlBaseDao, ICustomerRoleMappingRepository
    {
        public async Task AddToCustomer(CustomerRoleMapping[] customerRoleMappings, string customerId)
        {
            await WithConnection(async (connection, transaction) =>
           {
               DynamicParameters parameters = new DynamicParameters();
               parameters.Add("@CustomerId", customerId, DbType.String);
               var data = await connection.ExecuteAsync(ProcName.Customer_Role_Mapping_DeleteByCustomerId, parameters, transaction, commandType: CommandType.StoredProcedure);
               if (customerRoleMappings != null && customerRoleMappings.Length > 0)
               {
                   DataTable dataTable = CustomerRoleMapping.CreateDataTable();
                   foreach (var customerRoleMapping in customerRoleMappings)
                   {
                       customerRoleMapping.AddToDataTable(dataTable);
                   }
                   await BulkCopy(dataTable, connection, transaction);
               }
               return data;
           });
        }

        public async Task AddToRole(CustomerRoleMapping[] customerRoleMappings, string roleId)
        {
            await WithConnection(async (connection, transaction) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleId", roleId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Customer_Role_Mapping_DeleteByRoleId, parameters, transaction, commandType: CommandType.StoredProcedure);
                if (customerRoleMappings != null && customerRoleMappings.Length > 0)
                {
                    DataTable dataTable = CustomerRoleMapping.CreateDataTable();
                    foreach (var customerRoleMapping in customerRoleMappings)
                    {
                        customerRoleMapping.AddToDataTable(dataTable);
                    }
                    await BulkCopy(dataTable, connection, transaction);
                }
                return data;
            });
        }
    }
}