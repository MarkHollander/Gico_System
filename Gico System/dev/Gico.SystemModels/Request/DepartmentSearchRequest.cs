using Gico.Config;

namespace Gico.SystemModels.Request
{
    public class DepartmentSearchRequest
    {
        public string Name { get; set; }
        public EnumDefine.DepartmentStatusEnum Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}