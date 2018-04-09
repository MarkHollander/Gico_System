using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class AttrCategoryGetRequest : BaseRequest
    {
        public int AttributeId{ get; set; }
        public string CategoryId { get; set; }
    }
}
