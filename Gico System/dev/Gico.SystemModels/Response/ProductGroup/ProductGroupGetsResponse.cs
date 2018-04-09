using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;
using Gico.SystemModels.Models.ProductGroup;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupGetsResponse : BaseResponse
    {
        public ProductGroupGetsResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
        }
        public KeyValueTypeIntModel[] Statuses { get; }
        public ProductGroupViewModel[] ProductGroups { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        //public CategoryModel[] Categories { get; set; }


    }
}