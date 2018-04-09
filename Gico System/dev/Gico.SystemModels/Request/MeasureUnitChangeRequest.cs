using Gico.Config;
using Gico.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class MeasureUnitChangeRequest : BaseRequest
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string UpdatedUserId { get; set; }
        public string BaseUnitId { get; set; }
        public string Ratio { get; set; }
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
    }
}
