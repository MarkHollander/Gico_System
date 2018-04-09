using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class DepartmentGetResponse : BaseResponse
    {
        public DepartmentGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.DepartmentStatusEnum), false);
        }
        public DepartmentViewModel Department { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
    }
}