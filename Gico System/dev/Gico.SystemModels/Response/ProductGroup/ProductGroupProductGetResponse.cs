using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupProductGetResponse : BaseResponse
    {
        public ProductGroupProductGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.ProductType));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ProductGroupProductViewModel[] Products { get; set; }

        public KeyValueTypeIntModel[] Statuses { get; private set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
    }
}