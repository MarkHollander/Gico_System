using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.Banner
{
    public class BannerSearchRequest : BaseRequest
    {
        public string Id { get; set; }
        public string BannerName { get; set; }        
        public EnumDefine.CommonStatusEnum Status { get; set; }        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
