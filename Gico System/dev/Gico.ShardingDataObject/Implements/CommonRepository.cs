using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.CQRS.Service.Interfaces;
using Gico.ShardingDataObject.Interfaces;

namespace Gico.ShardingDataObject.Implements
{
    public class CommonRepository : SqlBaseDao, ICommonRepository
    {
        public async Task<long> GetNextValueForSequence(string pathName)
        {
            const string spName = "GetNextValueForSequence";
            return await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return await p.ExecuteScalarAsync<long>(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }
        public async Task CreateSequence(string pathName)
        {
            const string spName = "CreateSequenceByPathName";
            await WithConnection(async p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return await p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        
    }
}