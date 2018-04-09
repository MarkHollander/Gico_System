using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gico.Common;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Implements
{
    public class MeasureUnitRepository : SqlBaseDao, IMeasureUnitRepository
    {
        public async Task<RMeasureUnit> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RMeasureUnit>(ProcName.Locale_String_Resource_GetById, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }



        public async Task<RMeasureUnit[]> Search(string unitName, EnumDefine.GiftCodeCampaignStatus unitStatus, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UnitName", unitName, DbType.String);
                parameters.Add("@UnitStatus", unitStatus.AsEnumToInt(), DbType.String);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.Int32);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.Int32);
                var data = (await connection.QueryAsync<RMeasureUnit>(ProcName.Measure_Unit_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }

        public async Task Add(MeasureUnit measure)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UnitName", measure.UnitName, DbType.String);
                parameters.Add("CreatedOnUtc", measure.CreatedDateUtc, DbType.DateTime);
                parameters.Add("CreatedUserId", measure.CreatedUid, DbType.String);
                parameters.Add("BaseUnitId", measure.BaseUnitId, DbType.String);
                parameters.Add("Ratio", measure.Ratio, DbType.Decimal);
                parameters.Add("UnitStatus", measure.UnitStatus.AsEnumToInt(), DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Measure_Unit_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Change(MeasureUnit measure)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", measure.Id, DbType.String);
                parameters.Add("UnitName", measure.UnitName, DbType.String);
                parameters.Add("UpdatedOnUtc", measure.UpdatedDateUtc, DbType.DateTime);
                parameters.Add("UpdatedUserId", measure.UpdatedUid, DbType.String);
                parameters.Add("BaseUnitId", measure.BaseUnitId, DbType.String);
                parameters.Add("Ratio", measure.Ratio, DbType.Decimal);
                parameters.Add("UnitStatus", measure.UnitStatus.AsEnumToInt(), DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Measure_Unit_Change, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }
    }
}
