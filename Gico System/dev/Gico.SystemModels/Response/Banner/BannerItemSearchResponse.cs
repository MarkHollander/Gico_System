using Gico.Config;
using Gico.Models.Response;
using Gico.SystemModels.Models.Banner;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Response.Banner
{
    public class BannerItemSearchResponse : BaseResponse
    {
        public BannerItemSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.CommonStatusEnum));
            Banner = new BannerViewModel();
        }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public BannerViewModel Banner { get; set; }
        public BannerItemViewModel[] BannerItems { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; private set; }        
    }
}
