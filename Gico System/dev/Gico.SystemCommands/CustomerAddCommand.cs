using System;
using Gico.Config;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands
{
    public class CustomerAddCommand : Command
    {
        public CustomerAddCommand()
        {
        }

        public CustomerAddCommand(int version) : base(version)
        {
        }

        public string Email { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingAddressId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public EnumDefine.TwoFactorEnum TwoFactorEnabled { get; set; }
        public string FullName { get; set; }
        public EnumDefine.GenderEnum Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public EnumDefine.CustomerTypeEnum Type { get; set; }
        public EnumDefine.CustomerStatusEnum Status { get; set; }
        public string LastIpAddress { get; set; }
        public string Code { get; set; }
        public string CreatedUid { get; set; }
        public string LanguageId { get; set; }
    }
    public class CustomerChangeCommand : CustomerAddCommand
    {
        public CustomerChangeCommand()
        {
        }

        public CustomerChangeCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }

        public DateTime UpdatedDateUtc => this.CreatedDateUtc;

        public string UpdatedUid { get; set; }
    }
}