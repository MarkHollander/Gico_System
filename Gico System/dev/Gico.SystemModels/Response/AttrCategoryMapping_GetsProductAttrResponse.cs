using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class AttrCategoryMapping_GetsProductAttrResponse:BaseResponse
    {
        public AttrCategoryMapping_GetsProductAttrResponse()
        {
            this.AttributeType = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.AttrCategoryType), false);
        }
        public ProductAttributeViewModel[] ProductAttributes { get; set; }
        public KeyValueTypeIntModel[] AttributeType { get; private set; }
    }
}
