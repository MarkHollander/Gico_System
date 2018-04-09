using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response.ProductGroup
{
    public class ProductGroupCategoryGetResponse : BaseResponse
    {
        public JsTreeModel[] Categories { get; set; }
        public string[] CategoryIds { get; set; }
    }

    
}