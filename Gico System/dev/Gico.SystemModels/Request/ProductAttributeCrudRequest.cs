using Gico.Config;
using Gico.Models.Request;

namespace Gico.SystemModels.Request
{
    public class ProductAttributeCrudRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EnumDefine.StatusEnum Status { get; set; }
    }

    public class ProductAttributeValueCrudRequest : BaseRequest
    {
        public string Id { get; set; }
        public string AttributeId { get; set; }
        public string Value { get; set; }
        public int UnitId { get; set; }
        public EnumDefine.StatusEnum Status { get; set; }
        public int Order { get; set; }
    }
}