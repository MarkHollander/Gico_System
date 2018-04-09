using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class VendorSearchResponse : BaseResponse
    {
        public VendorSearchResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.VendorTypeEnum));
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.VendorStatusEnum));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public VendorViewModel[] Vendors { get; set; }

        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
    }
}