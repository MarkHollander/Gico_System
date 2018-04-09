using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.Banner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.Banner
{
    public class BannerSearchResponse : BaseResponse
    {
        public BannerSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));            
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public BannerViewModel[] Banners { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }        
    }
}
