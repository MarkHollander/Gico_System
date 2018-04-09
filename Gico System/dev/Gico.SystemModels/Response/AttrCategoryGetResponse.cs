using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class AttrCategoryGetResponse : BaseResponse
    {
        public AttrCategoryModel AttrCategory { get; set; }
        public AttrCategoryGetResponse()
        {
            AttributeType = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.AttrCategoryType), false);

        }
       
        public ProductAttributeViewModel[] ProductAttributes { get; set; }
        public KeyValueTypeIntModel[] AttributeType { get; private set; }
    }
}
