using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands
{
    public class MeasureUnitAddCommand : Command
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string BaseUnitId { get; set; }
        public string Ratio { get; set; }
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
    }
    public class MeasureUnitChangeCommand : MeasureUnitAddCommand
    {
        public DateTime UpdatedOnUtc { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
