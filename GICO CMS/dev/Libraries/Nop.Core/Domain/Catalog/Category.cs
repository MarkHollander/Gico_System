using System;
using System.Collections.Generic;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public partial class Category : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported
    {        
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public string ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string CategoryDescription { get; set; }

        /// <summary>
        /// Gets or sets the Category Status
        /// </summary>
        public int CategoryStatus { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the category code
        /// </summary>
        public string CategoryCode { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }
        public int Discount_Id { get; set; }
        public enum StatusEnum
        {
            Deleted = -1,
            New = 3,
            Active = 1,
            Inactive = 2
        }
    }
}
