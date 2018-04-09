using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request.ProductGroup
{
    public class ProductGroupAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string Description { get; set; }

    }
}