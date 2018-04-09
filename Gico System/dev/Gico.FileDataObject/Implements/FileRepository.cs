using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.CQRS.Service.Interfaces;
using Gico.FileDataObject.Interfaces;
using Gico.FileDomains;

namespace Gico.FileDataObject.Implements
{
    public class FileRepository : SqlBaseDao, IFileRepository
    {
        public async Task Add(string connectionString, File file)
        {
            const string spName = "File_Add";
            await WithConnection(connectionString, async (connection) =>
             {
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@Id", file.Id, DbType.String);
                 parameters.Add("@ShardId", file.ShardId, DbType.Int32);
                 parameters.Add("@ParentId", file.ParentId, DbType.String);
                 parameters.Add("@FileName", file.FileName, DbType.String);
                 parameters.Add("@Extentsion", file.Extension, DbType.String);
                 parameters.Add("@TYPE", file.Type, DbType.String);
                 parameters.Add("@FILEPATH", file.FilePath, DbType.String);
                 parameters.Add("@Info", file.Info, DbType.String);
                 parameters.Add("@CreatedDateUtc", file.CreatedDateUtc, DbType.DateTime);
                 parameters.Add("@UpdatedDateUtc", file.CreatedDateUtc, DbType.DateTime);
                 parameters.Add("@CreatedUid", file.CreatedUid, DbType.String);
                 parameters.Add("@UpdatedUid", file.CreatedUid, DbType.String);
                 return await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
             });
        }
    }
}
