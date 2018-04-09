using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Banner
{
    public class BannerAddCommand : Command
    {
        public BannerAddCommand()
        {
        }

        #region Instance Properties        
        public string BannerName { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string BackgroundRGB { get; set; }
        public string CreatedUid { get; set; }

        #endregion Instance Properties
    }
}
