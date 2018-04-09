using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using System;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        private ICollection<VendorNote> _vendorNotes;

        public string VendorId { get; set; }

        public string VendorCode { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string VendorDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public string VendorLogo { get; set; }

        /// <summary>
        /// Gets or sets the address identifier
        /// </summary>
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public Int16 VendorStatus { get; set; }
        /// <summary>
        /// Gets or sets the category code
        /// </summary>
        public string CreatedUserId { get; set; }

        /// <summary>
        /// Gets or sets the category code
        /// </summary>
        public string UpdatedUserId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
        public Int16 VendorType { get; set; }
        private bool CheckStatus(StatusEnum status)
        {
            return VendorStatus == (int)status;
        }

        public bool IsDeleted => CheckStatus(StatusEnum.Deleted);
        public bool IsActive => CheckStatus(StatusEnum.Active);
        public enum StatusEnum
        {
            Deleted = -1,
            New = 3,
            Active = 1,
            Inactive = 2
        }
    }
}
