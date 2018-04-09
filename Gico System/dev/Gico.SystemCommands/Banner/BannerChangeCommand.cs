using System;
using System.Collections.Generic;
using System.Text;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.Banner
{
    public class BannerChangeCommand : Command
    {
        public BannerChangeCommand()
        {
        }

        public string Id { get; set; }
        public string BannerName { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public string BackgroundRGB { get; set; }
        public string UpdatedUid { get; set; }
    }
}
