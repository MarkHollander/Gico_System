using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models;

namespace Gico.SystemModels.Response
{
    public class CustomerSearchResponse : BaseResponse
    {
        public CustomerSearchResponse()
        {
            Types = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CustomerTypeEnum));
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CustomerStatusEnum));
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerViewModel[] Customers { get; set; }

        public KeyValueTypeIntModel[] Types { get; private set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }
    }
}