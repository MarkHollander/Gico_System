using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class CategoryAttrResponse : BaseResponse
    {
        public CategoryAttrModel[] CategoryAttrs { get; set; }
        public CategoryAttrResponse()
        {
            AttributeType = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.AttrCategoryType), false);
            
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public KeyValueTypeIntModel[] AttributeType { get; private set; }
      
    }
}
