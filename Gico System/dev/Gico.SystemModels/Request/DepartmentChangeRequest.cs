using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class DepartmentChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }
}