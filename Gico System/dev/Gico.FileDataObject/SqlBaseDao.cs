using Gico.CQRS.Service.Interfaces;
using Gico.DataObject;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.Config;
using Gico.Domains;

namespace Gico.FileDataObject
{
    public class SqlBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbFileConnectionString.GetConfig();
    }
}
