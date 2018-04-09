using Gico.Config;

namespace Gico.SystemModels.Request
{
    public class RoleSearchRequest
    {
        public string DepartmentId { get; set; }
        public EnumDefine.RoleStatusEnum Status { get; set; }
        public string Name { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}