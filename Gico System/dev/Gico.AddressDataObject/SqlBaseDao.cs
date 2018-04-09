using Gico.Config;
using Gico.DataObject;

namespace Gico.AddressDataObject
{
    public interface ISqlBaseDao
    {
    }
    public class SqlBaseDao : BaseDao, ISqlBaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbAddressConnectionString.GetConfig();


        public class ProcName
        {
            #region Address

            public const string Province_GetAll = "Province_GetAll";
            public const string District_GetByProvinceId = "District_GetByProvinceId";
            public const string Street_GetByWardId = "Street_GetByWardId";
            public const string Ward_GetByDistrictId = "Ward_GetByDistrictId";

            public const string District_Add = "District_Add";
            public const string District_Delete = "District_Delete";
            public const string District_GetById = "District_GetById";
            public const string District_Update = "District_Update";
            public const string District_ChangeStatus = "District_ChangeStatus";

            public const string Province_Update = "Province_Update";
            public const string Province_GetById = "Province_GetById";
            public const string Province_ChangeStatus = "Province_ChangeStatus";

            public const string Ward_GetById = "Ward_GetById";
            public const string Ward_ChangeStatus = "Ward_ChangeStatus";
            public const string Ward_Update = "Ward_Update";

            public const string Street_GetById = "Street_GetById";
            public const string Street_ChangeStatus = "Street_ChangeStatus";

            #endregion


        }
    }
}
