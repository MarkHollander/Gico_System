using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class RoleSearchResponse : BaseResponse
    {
        public RoleSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.RoleStatusEnum));
        }
        public RoleViewModel[] Roles { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public KeyValueTypeStringModel[] Departments { get; set; }
    }
}