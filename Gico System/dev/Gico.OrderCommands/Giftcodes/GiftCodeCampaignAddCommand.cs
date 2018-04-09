using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.OrderCommands.Giftcodes
{
    public class GiftCodeCampaignAddCommand : Command
    {
        public GiftCodeCampaignAddCommand()
        {

        }

        public GiftCodeCampaignAddCommand(int version) : base(version)
        {
        }
        
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AllowPaymentOnCheckout { get; set; }
        public EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public string CreatedUid { get; set; }
        public GiftCodeCalendarCommand[] Calendars { get; set; }

        public GiftCodeConditionCommand[] Conditions { get; set; }

    }

    public class GiftCodeCampaignChangeCommand: GiftCodeCampaignAddCommand
    {
        public string Id { get; set; }
    }

    public class GiftCodeCampaignChangeStatusCommand : Command
    {
        public string Id { get; set; }
        public EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public string CreatedUid { get; set; }
    }

    public class GiftCodeCalendarCommand
    {
        public DateTime Date { get; set; }
        public int[] Times { get; set; }
    }
    public class GiftCodeConditionCommand
    {
        public EnumDefine.GiftCodeConditionTypeEnum ConditionType { get; set; }
        public object Condition { get; set; }

    }
}