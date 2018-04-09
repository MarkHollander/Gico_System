using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    public class DepartmentSearchResponse : BaseResponse
    {
        public DepartmentSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.DepartmentStatusEnum));
        }
        public DepartmentViewModel[] Departments { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; }
    }
}