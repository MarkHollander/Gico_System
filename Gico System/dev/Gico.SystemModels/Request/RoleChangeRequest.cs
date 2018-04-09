using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class RoleChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string DepartmentId { get; set; }
    }
}