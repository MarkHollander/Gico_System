using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class DepartmentAddRequest : BaseRequest
    {
        public string Name { get; set; }
        public  bool Status { get; set; }

    }
}