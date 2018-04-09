using System;
using System.Collections.Generic;
using System.Linq;
using Gico.Common;
using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;
using static Gico.Config.EnumDefine;

namespace Gico.SystemDomains
{
    public class Customer : BaseDomain, IVersioned
    {
        public Customer(int version)
        {
            Version = version;
        }

        public Customer(RCustomer customer)
        {
            ShardId = customer.ShardId;
            Version = customer.Version;
            Id = customer.Id;
            Code = customer.Code;
            CreatedDateUtc = customer.CreatedDateUtc;
            CreatedUid = customer.CreatedUid;
            UpdatedDateUtc = customer.UpdatedDateUtc;
            UpdatedUid = customer.UpdatedUid;
            LanguageId = customer.LanguageId;
            StoreId = customer.StoreId;
            Status = customer.Status;

            Email = customer.Email;
            EmailConfirmed = customer.EmailConfirmed;
            IsTaxExempt = customer.IsTaxExempt;
            FailedLoginAttempts = customer.FailedLoginAttempts;
            LastIpAddress = customer.LastIpAddress;
            Password = customer.Password;
            PasswordSalt = customer.PasswordSalt;
            PhoneNumber = customer.PhoneNumber;
            PhoneNumberConfirmed = customer.PhoneNumberConfirmed;
            TwoFactorEnabled = customer.TwoFactorEnabled;
            FullName = customer.FullName;
            Gender = customer.Gender;
            Birthday = customer.Birthday;
            Type = customer.Type;
            Status = customer.Status;
            Version = customer.Version;
            EmailToRevalidate = customer.EmailToRevalidate;
            AdminComment = customer.AdminComment;
            BillingAddressId = customer.BillingAddressId;
            ShippingAddressId = customer.ShippingAddressId;
            ExternalLogins = customer.CustomerExternalLogins?.Select(p => new CustomerExternalLogin(p)).ToList();
        }

        #region Publish method

        public void Register(CustomerRegisterCommand command)
        {
            Id = Common.Common.GenerateGuid();
            Code = Common.Common.GenerateCodeFromId(command.SystemNumericalOrder, 3);
            string passwordHash = EncryptionExtensions.Encryption(Code, command.Password, out string salt);
            CreatedDateUtc = command.CreatedDateUtc;
            CreatedUid = string.Empty;
            UpdatedDateUtc = command.CreatedDateUtc;
            UpdatedUid = string.Empty;
            Email = command.Email;
            PhoneNumber = command.Mobile;
            EmailConfirmed = false;
            IsTaxExempt = false;
            FailedLoginAttempts = 0;
            LastIpAddress = command.RegisterIp;
            Password = passwordHash;
            PasswordSalt = salt;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = EnumDefine.TwoFactorEnum.Disable;
            FullName = command.FullName;
            Gender = command.Gender;
            Birthday = command.Birthday;
            AddType(CustomerTypeEnum.IsCustomer);
            Status = CustomerStatusEnum.New;
            RegisterEvent();
        }

        public void Add(CustomerAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            Code = command.Code;
            string passwordHash = EncryptionExtensions.Encryption(Code, command.Password, out string salt);
            Email = command.Email ?? string.Empty;
            EmailToRevalidate = string.Empty;
            EmailConfirmed = false;
            AdminComment = command.AdminComment ?? string.Empty;
            IsTaxExempt = command.IsTaxExempt;
            LastIpAddress = command.LastIpAddress;
            BillingAddressId = command.BillingAddressId ?? string.Empty;
            ShippingAddressId = command.ShippingAddressId ?? string.Empty;
            Password = passwordHash;
            PasswordSalt = salt;
            PhoneNumber = command.PhoneNumber ?? string.Empty;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = command.TwoFactorEnabled;
            FullName = command.FullName ?? string.Empty;
            Gender = command.Gender;
            Birthday = command.Birthday;
            Type = command.Type;
            Status = command.Status;
            CreatedDateUtc = command.CreatedDateUtc;
            LanguageId = command.LanguageId ?? string.Empty;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
            Version = command.Version;
        }
        public void Change(CustomerChangeCommand command)
        {
            Add(command);
            Id = command.Id ?? string.Empty;
            UpdatedDateUtc = command.UpdatedDateUtc;
            UpdatedUid = command.UpdatedUid ?? string.Empty;
        }
        public void Active()
        {
            Status = CustomerStatusEnum.Active;
        }
        public void Lock()
        {
            Status = CustomerStatusEnum.Lock;
        }
        public void AddType(CustomerTypeEnum type)
        {
            Type |= type;
        }
        public void RemoveType(CustomerTypeEnum type)
        {
            Type &= ~type;
        }
        public bool CheckType(CustomerTypeEnum type)
        {
            return Type.HasFlag(type);
        }
        public void Change(string fullName, string mobile, EnumDefine.GenderEnum gender, DateTime birthday)
        {
            FullName = fullName;
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                PhoneNumber = mobile;
            }
            Gender = gender;
            Birthday = birthday;
        }

        public CustomerExternalLogin AddExternalLogin(CustomerExternalLoginAddCommand command)
        {
            if (ExternalLogins == null)
            {
                ExternalLogins = new List<CustomerExternalLogin>();
            }
            if (ExternalLogins.Any(
                p => p.LoginProvider == command.LoginProvider && p.ProviderKey == command.ProviderKey))
            {
                return null;
            }
            CustomerExternalLogin customerExternalLogin = new CustomerExternalLogin(command.LoginProvider,
                command.ProviderKey, command.ProviderDisplayName, command.CustomerId, command.Info);
            ExternalLogins.Add(customerExternalLogin);
            Version = command.Version;
            return customerExternalLogin;
        }
        #endregion

        #region Event
        private void RegisterEvent()
        {
            CustomerCacheRegisterEvent @event = new CustomerCacheRegisterEvent();
            //@event.Registered(Id, Email, PasswordHash, CreatedDateUtc);
            AddEvent(@event);
        }
        #endregion

        #region Properties
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        // Miễn thuế
        public bool IsTaxExempt { get; private set; }
        public int FailedLoginAttempts { get; private set; }
        public string LastIpAddress { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool PhoneNumberConfirmed { get; private set; }
        public EnumDefine.TwoFactorEnum TwoFactorEnabled { get; private set; }
        public string GoogleAuthenticatorSecretKey { get; private set; }
        public string FullName { get; private set; }
        public EnumDefine.GenderEnum Gender { get; private set; }
        public DateTime? Birthday { get; private set; }
        public EnumDefine.CustomerTypeEnum Type { get; private set; }
        public new EnumDefine.CustomerStatusEnum Status { get; private set; }
        public int Version { get; private set; }
        public string EmailToRevalidate { get; set; }
        public string AdminComment { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingAddressId { get; set; }

        public IList<CustomerExternalLogin> ExternalLogins { get; set; }
        #endregion


    }
}