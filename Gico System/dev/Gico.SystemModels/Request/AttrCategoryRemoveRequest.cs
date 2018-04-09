using Gico.Config;
using Gico.Models.Request;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Request
{
    public class AttrCategoryRemoveRequest : BaseRequest
    {

        public int AttributeId { get; set; }

        public string CategoryId { get; set; }
    }
}
