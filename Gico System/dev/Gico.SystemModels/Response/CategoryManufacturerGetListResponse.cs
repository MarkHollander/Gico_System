using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class CategoryManufacturerGetListResponse : BaseResponse
    {
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public ManufacturerViewModel[] Manufacturers { get; set; }
    }
}
