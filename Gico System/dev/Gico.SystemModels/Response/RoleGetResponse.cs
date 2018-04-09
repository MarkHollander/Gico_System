using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class RoleGetResponse : BaseResponse
    {
        public RoleGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.RoleStatusEnum), false);
        }
        public RoleViewModel Role { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
    }
}