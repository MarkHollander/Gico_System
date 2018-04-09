using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class ProductAttributeGetsResponse : BaseResponse
    {
        public ProductAttributeGetsResponse()
        {
            this.AttributeType = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.AttrCategoryType), false);
        }
        public ProductAttributeModel[] ProductAttributes { get; set; }
        public KeyValueTypeIntModel[] AttributeType { get; private set; }
    }
}
