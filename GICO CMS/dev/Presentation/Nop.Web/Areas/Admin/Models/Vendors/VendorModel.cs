using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Validators.Vendors;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    [Validator(typeof(VendorValidator))]
    public partial class VendorModel : BaseNopEntityModel, ILocalizedModel<VendorLocalizedModel>
    {
        public VendorModel()
        {
            Locales = new List<VendorLocalizedModel>();
            AvailableVendorStatus = new List<SelectListItem>();
        }

        public string VendorId { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        public string VendorName { get; set; }

        [DataType(DataType.EmailAddress)]
        [NopResourceDisplayName("Admin.Vendors.Fields.Email")]
        public string Email { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Description")]
        public string VendorDescription { get; set; }
        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Vendors.Fields.Picture")]
        public string VendorLogo { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.SeName")]
        public string SeName { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.CompanyName")]
        public string CompanyName { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Phone")]
        public string Phone { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Fax")]
        public string Fax { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Website")]
        public string Website { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.VendorStatus")]
        public int VendorStatus { get; set; }        
        [NopResourceDisplayName("Admin.Vendors.Fields.VendorType")]
        public int VendorType { get; set; }
        public IList<VendorLocalizedModel> Locales { get; set; }
        public IList<SelectListItem> AvailableVendorStatus { get; set; }
    }

    public partial class VendorLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        public string VendorName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Description")]
        public string VendorDescription { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.SeName")]
        public string SeName { get; set; }
    }
}