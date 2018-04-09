using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.Banner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.Banner
{
    public class BannerGetResponse : BaseResponse
    {
        public BannerGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));            
        }
        public BannerViewModel Banner { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }        
    }
}
