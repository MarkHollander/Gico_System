using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.Banner
{
    public class BannerItemChangeCommand : Command
    {
        public BannerItemChangeCommand()
        {
        }

        public string Id { get; set; }
        #region Instance Properties        
        public string BannerItemName { get; set; }
        public string BannerId { get; set; }
        public string TargetUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public bool IsDefault { get; set; }
        public string BackgroundRGB { get; set; }
        public string UpdatedUid { get; set; }
        #endregion Instance Properties
    }
}
