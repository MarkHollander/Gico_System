using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request.Banner
{    
    public class BannerAddOrChangeRequest : BaseRequest
    {
        public string Id { get; set; }        
        public string BannerName { get; set; }                
        public string BackgroundRGB { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }        
    }
}
