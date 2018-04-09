using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class Customer : BaseEntity
    {
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ReturnRequest> _returnRequests;
        private ICollection<Address> _addresses;

        /// <summary>
        /// Ctor
        /// </summary>
        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
        }
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the customer GUID
        /// </summary>
        public Guid CustomerGuid { get; set; }
        public string Email { get; set; }
        public string EmailToRevalidate { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public string LastIpAddress { get; set; }
        public string BillingAddressId { get; set; }
        public string ShippingAddressId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public TwoFactorEnum TwoFactorEnabled { get; set; }
        public string FullName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public CustomerTypeEnum Type { get; set; }
        public long Status { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string LanguageId { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
        public string CreatedUid { get; set; }
        public string UpdatedUid { get; set; }
        public int Version { get; set; }

        #region Navigation properties

        /// <summary>
        /// Gets or sets customer generated content
        /// </summary>
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get { return _externalAuthenticationRecords ?? (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>()); }
            protected set { _externalAuthenticationRecords = value; }
        }

        /// <summary>
        /// Gets or sets the customer roles
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }

        /// <summary>
        /// Gets or sets return request of this customer
        /// </summary>
        public virtual ICollection<ReturnRequest> ReturnRequests
        {
            get { return _returnRequests ?? (_returnRequests = new List<ReturnRequest>()); }
            protected set { _returnRequests = value; }
        }

        /// <summary>
        /// Default billing address
        /// </summary>
        //public virtual Address BillingAddress { get; set; }

        /// <summary>
        /// Default shipping address
        /// </summary>
        ///public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets customer addresses
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }
        }

        #endregion
        public enum GenderEnum
        {
            Male = 1,
            Female = 2,
            Other = 3
        }
        public enum CustomerStatusEnum
        {
            New = 1,
            Active = 2,
            Lock = 3
        }
        [Flags]
        public enum CustomerTypeEnum
        {
            None = 0,
            IsEmployee = 1,
            IsCustomer = 2,
            IsCustomerVip1 = 4,
            IsCustomerVip2 = 8,
        }
        public enum TwoFactorEnum
        {
            None = 0,
            Email = 1,
            Sms = 2
        }

    }
}