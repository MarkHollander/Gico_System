using System;
using Gico.Common;
using Gico.Config;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Request
{
    public class GiftCodeCampaignAddOrChangeRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }

        public bool AllowPaymentOnCheckout { get; set; }
        public GiftCodeCalendarViewModel[] Calendars { get; set; }
        public GiftCodeConditionViewModel[] Conditions { get; set; }

        public DateTime? BeginDateValue => BeginDate.AsDateTimeNullable(SystemDefine.DateTimeFormat);

        public DateTime? EndDateValue => BeginDate.AsDateTimeNullable(SystemDefine.DateTimeFormat);

        public int ShardId { get; set; }
        public int Version { get; set; }

    }
}