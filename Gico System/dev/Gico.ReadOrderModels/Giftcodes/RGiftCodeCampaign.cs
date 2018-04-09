using System;

namespace Gico.ReadOrderModels.Giftcodes
{
    public class RGiftCodeCampaign : BaseReadModel
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public string GiftCodeCalendar
        {
            get => GiftCodeCalendarObject == null
                ? string.Empty
                : Common.Serialize.JsonSerializeObject(GiftCodeCalendarObject);
            set => GiftCodeCalendarObject = string.IsNullOrEmpty(value) ? null : Common.Serialize.JsonDeserializeObject<RGiftCodeCalendar[]>(value);
        }

        public string Message { get; set; }
        public bool AllowPaymentOnCheckout { get; set; }
        public string Conditions
        {
            get => GiftCodeConditionObject == null
                ? string.Empty
                : Common.Serialize.JsonSerializeObject(GiftCodeConditionObject);
            set => GiftCodeConditionObject = string.IsNullOrEmpty(value) ? null : Common.Serialize.JsonDeserializeObject<RGiftCodeCondition[]>(value);
        }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ApprovedUid { get; set; }
        public RGiftCodeCalendar[] GiftCodeCalendarObject { get; set; }
        public RGiftCodeCondition[] GiftCodeConditionObject { get; set; }

    }
}