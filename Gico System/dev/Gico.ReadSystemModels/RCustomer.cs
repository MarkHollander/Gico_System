using Gico.Config;
using System;
using ProtoBuf;

namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RCustomer : BaseReadModel
    {
        #region Properties
        [ProtoMember(1)]
        public string Email { get; set; }
        [ProtoMember(2)]
        public bool EmailConfirmed { get; set; }
        // Miễn thuế
        [ProtoMember(3)]
        public bool IsTaxExempt { get; set; }
        [ProtoMember(4)]
        public int FailedLoginAttempts { get; set; }
        [ProtoMember(5)]
        public string LastIpAddress { get; set; }
        [ProtoMember(6)]
        public DateTime LastLoginDateUtc { get; set; }
        [ProtoMember(7)]
        public string Password { get; set; }
        [ProtoMember(8)]
        public string PasswordSalt { get; set; }
        [ProtoMember(9)]
        public string PhoneNumber { get; set; }
        [ProtoMember(10)]
        public bool PhoneNumberConfirmed { get; set; }
        [ProtoMember(11)]
        public EnumDefine.TwoFactorEnum TwoFactorEnabled { get; set; }
        [ProtoMember(12)]
        public string FullName { get; set; }
        [ProtoMember(13)]
        public EnumDefine.GenderEnum Gender { get; set; }
        [ProtoMember(14)]
        public DateTime? Birthday { get; set; }
        [ProtoMember(15)]
        public EnumDefine.CustomerTypeEnum Type { get; set; }
        [ProtoMember(16)]
        public new EnumDefine.CustomerStatusEnum Status { get; set; }
        [ProtoMember(17)]
        public string EmailToRevalidate { get; set; }
        [ProtoMember(18)]
        public string AdminComment { get; set; }
        [ProtoMember(19)]
        public string BillingAddressId { get; set; }
        [ProtoMember(20)]
        public string ShippingAddressId { get; set; }
        [ProtoMember(21)]
        public RCustomerExternalLogin[] CustomerExternalLogins { get; set; }
        #endregion
    }
    [ProtoContract]
    public class RCustomerExternalLogin
    {

        [ProtoMember(1)]
        public EnumDefine.CutomerExternalLoginProviderEnum LoginProvider { get; set; }
        [ProtoMember(2)]
        public string ProviderKey { get; set; }
        [ProtoMember(3)]
        public string ProviderDisplayName { get; set; }
        [ProtoMember(4)]
        public string CustomerId { get; set; }
        [ProtoMember(5)]
        public string Info { get; set; }

    }
}