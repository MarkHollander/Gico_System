using System;
using System.Linq;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.OrderCommands.Giftcodes;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCodeCampaign : BaseDomain, IVersioned
    {
        public void Add(GiftCodeCampaignAddCommand command, int shardId)
        {
            Name = command.Name;
            Notes = command.Notes;
            BeginDate = command.BeginDate;
            EndDate = command.EndDate;
            GiftCodeCalendars = command.Calendars.Select(p => new GiftCodeCalendar(p.Date, p.Times)).ToArray();
            Message = string.Empty;
            AllowPaymentOnCheckout = command.AllowPaymentOnCheckout;
            GiftCodeConditions = command.Conditions.Select(p => new GiftCodeCondition(p.ConditionType, p.Condition))
                .ToArray();
            Status = EnumDefine.GiftCodeCampaignStatus.New;
            CreatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.CreatedUid;
            ShardId = shardId;
            Version = SystemDefine.DefaultVersion;
        }
        public void Change(GiftCodeCampaignChangeCommand command)
        {
            Id = command.Id;
            Name = command.Name;
            Notes = command.Notes;
            BeginDate = command.BeginDate;
            EndDate = command.EndDate;
            GiftCodeCalendars = command.Calendars.Select(p => new GiftCodeCalendar(p.Date, p.Times)).ToArray();
            Message = string.Empty;
            AllowPaymentOnCheckout = command.AllowPaymentOnCheckout;
            GiftCodeConditions = command.Conditions.Select(p => new GiftCodeCondition(p.ConditionType, p.Condition))
                .ToArray();
            Status = EnumDefine.GiftCodeCampaignStatus.New;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.CreatedUid;
            Version = command.Version;
        }
        public bool ChangeStatus(GiftCodeCampaignChangeStatusCommand command)
        {
            Id = command.Id;
            Status = command.Status;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = command.CreatedUid;
            Version = command.Version;
            switch (command.Status)
            {
                case EnumDefine.GiftCodeCampaignStatus.Active:
                case EnumDefine.GiftCodeCampaignStatus.ReActive:
                    ApprovedDate = command.CreatedDateUtc;
                    ApprovedUid = command.CreatedUid;
                    return true;
                    break;
            }
            return false;
        }

        public string Name { get; private set; }
        public string Notes { get; private set; }
        public DateTime BeginDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public GiftCodeCalendar[] GiftCodeCalendars { get; private set; }
        public string Message { get; private set; }
        public new EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public bool AllowPaymentOnCheckout { get; private set; }
        public GiftCodeCondition[] GiftCodeConditions { get; private set; }
        public DateTime? ApprovedDate { get; private set; }
        public string ApprovedUid { get; private set; }

        public GiftCodeGroup[] GiftCodeGroups { get; private set; }

        public int Version { get; private set; }

        public string GiftCodeCalendarsSerialize => Common.Serialize.JsonSerializeObject(GiftCodeCalendars);
        public string GiftCodeConditionsSerialize => Common.Serialize.JsonSerializeObject(GiftCodeConditions);
    }
}