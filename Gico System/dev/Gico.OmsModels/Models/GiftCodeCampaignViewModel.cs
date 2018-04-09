using System;
using Gico.Config;
using Gico.OmsModels.Request;

namespace Gico.OmsModels.Models
{
    public class GiftCodeCampaignViewModel
    {
        public GiftCodeCampaignViewModel()
        {
            CalendarMode = 1;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public bool AllowPaymentOnCheckout { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedUid { get; set; }
        public EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public GiftCodeCalendarViewModel Calendar { get; set; }
        public int CalendarMode { get; set; }
        public GiftCodeConditionViewModel[] Conditions { get; set; }
        public int ShardId { get; set; }
        public int Version { get; set; }
    }
    public class GiftCodeConditionViewModel
    {
        public EnumDefine.GiftCodeConditionTypeEnum ConditionType { get; set; }
        public string Condition { get; set; }
    }
    public class GiftCodeCalendarViewModel
    {
        public DateTime Date { get; set; }
        public int[] Times { get; set; }
    }
}