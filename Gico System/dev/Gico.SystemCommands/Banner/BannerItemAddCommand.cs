using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Banner
{
    public class BannerItemAddCommand : Command
    {
        public BannerItemAddCommand()
        {
        }

        #region Instance Properties        
        public string BannerItemName { get; set; }
        public string BannerId { get; set; }
        public string TargetUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        public bool IsDefault { get; set; }
        public string BackgroundRGB { get; set; }
        public string CreatedUid { get; set; }
        #endregion Instance Properties
    }
}
