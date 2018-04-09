using System;
using Gico.Common;
using Gico.Config;

namespace Gico.OmsModels.Request
{
    public class GiftCodeCampaignGetsRequest
    {
        public string CampaignName { get; set; }
        public string Giftcode { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DateTime? BeginDateValue => BeginDate.AsDateTimeNullable(SystemDefine.DateFormat);

        public DateTime? EndDateValue => BeginDate.AsDateTimeNullable(SystemDefine.DateFormat);
    }
}