using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class MeasureUnitAddRequest : BaseRequest
    {
        public string UnitName { get; set; }
        public string CreatedUserId { get; set; }
        public string BaseUnitId { get; set; }
        public string Ratio { get; set; }
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
    }
}
