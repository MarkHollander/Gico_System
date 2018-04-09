﻿using System.Data;
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
    public class RoleRepository : SqlBaseDao, IRoleRepository
    {
        public async Task<RRole[]> GetByDepartmentId(string departmentId)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DepartmentId", departmentId, DbType.String);
                var data = await connection.QueryAsync<RRole>(ProcName.Role_GetByDepartmentId, parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<RRole[]> Search(string name, EnumDefine.RoleStatusEnum status, string departmentId, RefSqlPaging sqlPaging)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", name, DbType.String);
                parameters.Add("@Status", status.AsEnumToInt(), DbType.String);
                parameters.Add("@DepartmentId", departmentId, DbType.String);
                parameters.Add("@OFFSET", sqlPaging.OffSet, DbType.String);
                parameters.Add("@FETCH", sqlPaging.PageSize, DbType.String);
                var data = (await connection.QueryAsync<RRole>(ProcName.Role_Search, parameters, commandType: CommandType.StoredProcedure)).ToArray();
                if (data.Length > 0)
                {
                    sqlPaging.TotalRow = data[0].TotalRow;
                }
                return data;
            });
        }

        public async Task<RRole> GetById(string id)
        {
            return await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RRole>(ProcName.Role_GetById, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Add(Role role)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", role.Id, DbType.String);
                parameters.Add("@NAME", role.Name, DbType.String);
                parameters.Add("@STATUS", role.Status, DbType.Int64);
                parameters.Add("@DepartmentId", role.DepartmentId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Role_Add, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Change(Role role)
        {
            await WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", role.Id, DbType.String);
                parameters.Add("@NAME", role.Name, DbType.String);
                parameters.Add("@STATUS", role.Status, DbType.Int64);
                parameters.Add("@DepartmentId", role.DepartmentId, DbType.String);
                var data = await connection.ExecuteAsync(ProcName.Role_Change, parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }
    }
}