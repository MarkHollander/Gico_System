using System;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.ReadSystemModels.PageBuilder;
using Gico.SystemDomains.PageBuilder;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gico.MarketingDataObject.Implements.PageBuilder
{
    public class TemplateConfigRepository : SqlBaseDao, ITemplateConfigRepository
    {
        public async Task<RTemplateConfig> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RTemplateConfig>(ProcName.TemplateConfig_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RTemplateConfig[]> Search(string id, string templateId, EnumDefine.TemplateConfigComponentTypeEnum componentType, EnumDefine.CommonStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", string.IsNullOrEmpty(id) ? string.Empty : id, DbType.String);
                parameters.Add("@TemplateId", string.IsNullOrEmpty(templateId) ? string.Empty : templateId, DbType.String);
                parameters.Add("@ComponentType", componentType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                parameters.Add("@DeletedStatus", EnumDefine.CommonStatusEnum.Deleted.AsEnumToInt(), DbType.Int32);
                var data = await connection.QueryAsync<RTemplateConfig>(ProcName.TemplateConfig_Search, parameters, commandType: CommandType.StoredProcedure);

                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }
        public async Task<RTemplateConfig[]> GetByTemplateId(string templateId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TemplateId", templateId, DbType.String);
                var data = await connection.QueryAsync<RTemplateConfig>(ProcName.TemplateConfig_GetByTemplateId, parameters, commandType: CommandType.StoredProcedure);
                var dataReturn = data.ToArray();
                return dataReturn;
            });
        }
        public async Task<bool> Add(TemplateConfig templateConfig)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", templateConfig.Id, DbType.String);
                parameters.Add("@TemplateId", templateConfig.TemplateId, DbType.String);
                parameters.Add("@TemplatePositionCode", templateConfig.TemplatePositionCode, DbType.String);
                parameters.Add("@ComponentType", templateConfig.ComponentType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@ComponentId", templateConfig.ComponentId, DbType.String);
                parameters.Add("@Status", templateConfig.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@PathToView", templateConfig.PathToView, DbType.String);
                parameters.Add("@DataSource", templateConfig.DataSource, DbType.String);
                parameters.Add("@CreatedDateUtc", templateConfig.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", templateConfig.CreatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.TemplateConfig_Add, parameters, commandType: CommandType.StoredProcedure);
                if (rowCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async Task<bool> Change(TemplateConfig templateConfig)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", templateConfig.Id, DbType.String);
                parameters.Add("@TemplateId", templateConfig.TemplateId, DbType.String);
                parameters.Add("@TemplatePositionCode", templateConfig.TemplatePositionCode, DbType.String);
                parameters.Add("@ComponentType", templateConfig.ComponentType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@ComponentId", templateConfig.ComponentId, DbType.String);
                parameters.Add("@Status", templateConfig.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@PathToView", templateConfig.PathToView, DbType.String);
                parameters.Add("@DataSource", templateConfig.DataSource, DbType.String);
                parameters.Add("@UpdatedDateUtc", templateConfig.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", templateConfig.UpdatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.TemplateConfig_Change, parameters, commandType: CommandType.StoredProcedure);
                if (rowCount != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public async Task ChangeStatus(string id, string userId, DateTime changeDate, EnumDefine.CommonStatusEnum status)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                parameters.Add("@UpdatedUid", userId, DbType.String);
                parameters.Add("@UpdatedDate", changeDate, DbType.DateTime);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int32);
                return await connection.ExecuteAsync(ProcName.TemplateConfig_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
