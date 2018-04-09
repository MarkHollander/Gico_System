using Gico.Config;
using Gico.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemModels.Models
{
    public class MeasureUnitViewModel : BaseViewModel
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string BaseUnitId { get; set; }
        public string Ratio { get; set; }
        public EnumDefine.GiftCodeCampaignStatus UnitStatus { get; set; }
        public string StatusName { get; set; }
        public string UnitNameB { get; set; }
    }
}
