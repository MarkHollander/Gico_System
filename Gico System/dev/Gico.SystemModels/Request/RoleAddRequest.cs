using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class RoleAddRequest : BaseRequest
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public string DepartmentId { get; set; }
    }
}