using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.ReadSystemModels.PageBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using Gico.Config;
using System.Linq;
using Gico.Common;
using Gico.SystemDomains.PageBuilder;

namespace Gico.MarketingDataObject.Implements.PageBuilder
{
    public class TemplateRepository : SqlBaseDao, ITemplateRepository
    {
        public async Task<RTemplate> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<RTemplate>(ProcName.Template_GetById, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<RTemplate[]> Search(string code, string templateName, EnumDefine.CommonStatusEnum status, RefSqlPaging paging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", string.IsNullOrEmpty(code) ? string.Empty : code, DbType.String);
                parameters.Add("@TemplateName", string.IsNullOrEmpty(templateName) ? string.Empty : templateName, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@OFFSET", paging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", paging.PageSize, DbType.Int32);
                parameters.Add("@DeletedStatus", EnumDefine.CommonStatusEnum.Deleted.AsEnumToInt(), DbType.Int32);
                var data = await connection.QueryAsync<RTemplate>(ProcName.Template_Search, parameters, commandType: CommandType.StoredProcedure);

                var dataReturn = data.ToArray();
                if (dataReturn.Length > 0)
                {
                    paging.TotalRow = dataReturn[0].TotalRow;
                }
                return dataReturn;
            });
        }

        public async Task<bool> Add(Template template)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", template.Id, DbType.String);
                parameters.Add("@TemplateName", template.TemplateName, DbType.String);
                parameters.Add("@Thumbnail", template.Thumbnail, DbType.String);
                parameters.Add("@Structure", template.Structure, DbType.String);
                parameters.Add("@PathToView", template.PathToView, DbType.String);
                parameters.Add("@Status", template.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@Code", template.Code, DbType.String);
                parameters.Add("@PageType", template.PageType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@PageParameters", template.PageParameters, DbType.String);
                parameters.Add("@CreatedDateUtc", template.CreatedDateUtc, DbType.DateTime);
                parameters.Add("@CreatedUid", template.CreatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Template_Add, parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<bool> Change(Template template)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", template.Id, DbType.String);
                parameters.Add("@TemplateName", template.TemplateName, DbType.String);
                parameters.Add("@Thumbnail", template.Thumbnail, DbType.String);
                parameters.Add("@Structure", template.Structure, DbType.String);
                parameters.Add("@PathToView", template.PathToView, DbType.String);
                parameters.Add("@Status", template.Status.AsEnumToInt(), DbType.Int16);
                parameters.Add("@Code", template.Code, DbType.String);
                parameters.Add("@PageType", template.PageType.AsEnumToInt(), DbType.Int32);
                parameters.Add("@PageParameters", template.PageParameters, DbType.String);
                parameters.Add("@UpdatedDateUtc", template.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("@UpdatedUid", template.UpdatedUid, DbType.String);
                var rowCount = 0;
                rowCount = await connection.ExecuteAsync(ProcName.Template_Change, parameters, commandType: CommandType.StoredProcedure);
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
               return await connection.ExecuteAsync(ProcName.Template_ChangeStatus, parameters, commandType: CommandType.StoredProcedure);
           });
        }
    }
}
