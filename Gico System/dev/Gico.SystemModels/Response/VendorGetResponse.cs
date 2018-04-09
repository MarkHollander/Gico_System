using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class VendorGetResponse : BaseResponse
    {
        public VendorGetResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.VendorTypeEnum), false);
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.VendorStatusEnum), false);
        }
        public VendorViewModel Vendor { get; set; }
        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
    }
}