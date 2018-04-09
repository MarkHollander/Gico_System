using Gico.Config;
using Gico.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Models.Banner
{
    public class BannerViewModel : BaseViewModel
    {
        public BannerViewModel()
        {
            
        }
        public string Id { get; set; }
        public string BannerName { get; set; }               
        public string BackgroundRgb { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }                
        public string StatusName => Status.ToString();
        public BannerItemViewModel[] BannerItems { get; set; }
    }
}
