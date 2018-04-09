using Gico.Config;
using Gico.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Request
{
    public class MeasureUnitSearchRequest
    {
        public string UnitName { get; set; }

        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
