using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.ProductGroup;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupGetResponse : BaseResponse
    {
        public ProductGroupGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
        }
        public KeyValueTypeIntModel[] Statuses { get; }
        public ProductGroupViewModel ProductGroup { get; set; }

    }
}