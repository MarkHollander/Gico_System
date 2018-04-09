using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Extensions;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Core;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class VendorController : BaseAdminController
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly VendorSettings _vendorSettings;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public VendorController(ICustomerService customerService, 
            ILocalizationService localizationService,
            IVendorService vendorService, 
            IPermissionService permissionService,
            IUrlRecordService urlRecordService,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService,
            IDateTimeHelper dateTimeHelper,
            VendorSettings vendorSettings,
            ICustomerActivityService customerActivityService,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IWorkContext workContext)
        {
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._vendorService = vendorService;
            this._permissionService = permissionService;
            this._urlRecordService = urlRecordService;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._pictureService = pictureService;
            this._dateTimeHelper = dateTimeHelper;
            this._vendorSettings = vendorSettings;
            this._customerActivityService = customerActivityService;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._workContext = workContext;
        }

        #endregion

        #region Utilities

        protected virtual void UpdatePictureSeoNames(Vendor vendor)
        {
            //var picture = _pictureService.GetPictureById(vendor.PictureId);
            //if (picture != null)
            //    _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(vendor.Name));
        }

        protected virtual void UpdateLocales(Vendor vendor, VendorModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(vendor,
                    x => x.VendorName,
                    localized.VendorName,
                    localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                    x => x.VendorDescription,
                    localized.VendorDescription,
                    localized.LanguageId);

                //search engine name
                var seName = vendor.ValidateSeName(localized.SeName, localized.VendorName, false);
                _urlRecordService.SaveSlug(vendor, seName, localized.LanguageId);
            }
        }

        protected virtual void PrepareVendorModel(VendorModel model, Vendor vendor, bool excludeProperties, bool prepareEntireAddressModel)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            //var address = _addressService.GetAddressById(vendor != null ? vendor.AddressId : 0);

            //if (vendor != null)
            //{
            //    if (!excludeProperties)
            //    {
            //        if (address != null)
            //        {
            //            model.Address = address.ToModel();
            //        }
            //    }

            //    //associated customer emails
            //    model.AssociatedCustomers = _customerService
            //        .GetAllCustomers(vendorId: vendor.Id)
            //        .Select(c => new VendorModel.AssociatedCustomerInfo()
            //        {
            //            Id = c.CustomerId,
            //            Email = c.Email
            //        })
            //        .ToList();
            //}
            
        }

        protected virtual void PrepareAllVendorStatus(VendorModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.AvailableVendorStatus.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.Status.Deleted"),
                Value = "-1"
            });
            model.AvailableVendorStatus.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.Status.New"),
                Value = "1"
            });
            model.AvailableVendorStatus.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.Status.Active"),
                Value = "1"
            });
            model.AvailableVendorStatus.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Common.Status.InActive"),
                Value = "2"
            });
        }
        #endregion

        #region Vendors

        //list
        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var model = new VendorListModel();
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, VendorListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedKendoGridJson();

            var vendors = _vendorService.GetAllVendors(model.SearchName, command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                Data = vendors.Select(x =>
                {
                    var vendorModel = x.ToModel();
                    PrepareVendorModel(vendorModel, x, false, false);
                    return vendorModel;
                }),
                Total = vendors.TotalCount,
            };

            return Json(gridModel);
        }

        //create
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();


            var model = new VendorModel();

            PrepareVendorModel(model, null, false, true);
            PrepareAllVendorStatus(model);
            //locales
            AddLocales(_languageService, model.Locales);
            
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Create(VendorModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                model.VendorId = Guid.NewGuid().ToString("N");               
                model.VendorStatus = (int)Vendor.StatusEnum.New;
                var vendor = model.ToEntity();
                vendor.UpdatedOnUtc = DateTime.UtcNow;
                vendor.CreatedOnUtc = DateTime.UtcNow;
                vendor.CreatedUserId = _workContext.CurrentCustomer.CustomerId;
                vendor.UpdatedUserId = _workContext.CurrentCustomer.CustomerId;
                _vendorService.InsertVendor(vendor);

                //activity log
                _customerActivityService.InsertActivity("AddNewVendor", _localizationService.GetResource("ActivityLog.AddNewVendor"), vendor.Id);

                //search engine name
                model.SeName = vendor.ValidateSeName(model.SeName, vendor.VendorName, true);
                _urlRecordService.SaveSlug(vendor, model.SeName, 0);

                //locales
                UpdateLocales(vendor, model);
                //update picture seo file name
                UpdatePictureSeoNames(vendor);

                SuccessNotification(_localizationService.GetResource("Admin.Vendors.Added"));
                
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareVendorModel(model, null, true, true);
            PrepareAllVendorStatus(model);
            return View(model);
        }

        //edit
        public virtual IActionResult Edit(string vendorId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(vendorId);
            if (vendor == null || vendor.VendorStatus== (int)Vendor.StatusEnum.Deleted)
                //No vendor found with the specified id
                return RedirectToAction("List");

            var model = vendor.ToModel();
            PrepareVendorModel(model, vendor, false, true);
            PrepareAllVendorStatus(model);

            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.VendorName = vendor.GetLocalized(x => x.VendorName, languageId, false, false);
                locale.VendorDescription = vendor.GetLocalized(x => x.VendorDescription, languageId, false, false);               
                locale.SeName = vendor.GetSeName(languageId, false, false);
            });

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(VendorModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(model.VendorId);
            if (vendor == null || vendor.VendorStatus== (int)Vendor.StatusEnum.Deleted)
                //No vendor found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {                
                //var prevPictureId = vendor.PictureId;                            
                vendor = model.ToEntity(vendor);
                vendor.UpdatedOnUtc = DateTime.UtcNow;
                vendor.UpdatedUserId = _workContext.CurrentCustomer.CustomerId;

                _vendorService.UpdateVendor(vendor);

                //activity log
                _customerActivityService.InsertActivity("EditVendor", _localizationService.GetResource("ActivityLog.EditVendor"), vendor.Id);

                //search engine name
                model.SeName = vendor.ValidateSeName(model.SeName, vendor.VendorName, true);
                _urlRecordService.SaveSlug(vendor, model.SeName, 0);

                //address
                //var address = _addressService.GetAddressById(vendor.AddressId);
                //if (address == null)
                //{
                //    address = model.Address.ToEntity();
                //    address.CreatedOnUtc = DateTime.UtcNow;
                //    //some validation
                //    if (address.CountryId == 0)
                //        address.CountryId = null;
                //    if (address.StateProvinceId == 0)
                //        address.StateProvinceId = null;

                //    _addressService.InsertAddress(address);
                //    vendor.AddressId = address.Id;
                //    _vendorService.UpdateVendor(vendor);
                //}
                //else
                //{
                //    address = model.Address.ToEntity(address);
                //    //some validation
                //    if (address.CountryId == 0)
                //        address.CountryId = null;
                //    if (address.StateProvinceId == 0)
                //        address.StateProvinceId = null;

                //    _addressService.UpdateAddress(address);
                //}


                //locales
                UpdateLocales(vendor, model);
                //delete an old picture (if deleted or updated)
                //if (prevPictureId > 0 && prevPictureId != vendor.PictureId)
                //{
                //    var prevPicture = _pictureService.GetPictureById(prevPictureId);
                //    if (prevPicture != null)
                //        _pictureService.DeletePicture(prevPicture);
                //}
                //update picture seo file name
                UpdatePictureSeoNames(vendor);

                SuccessNotification(_localizationService.GetResource("Admin.Vendors.Updated"));
                
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareVendorModel(model, vendor, true, true);
            PrepareAllVendorStatus(model);

            return View(model);
        }

        //delete
        [HttpPost]
        public virtual IActionResult Delete(string id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null)
                //No vendor found with the specified id
                return RedirectToAction("List");

            //clear associated customer references
            var associatedCustomers = _customerService.GetAllCustomers(vendorId: vendor.VendorId);
            foreach (var customer in associatedCustomers)
            {
                _customerService.UpdateCustomer(customer);
            }

            //delete a vendor
            _vendorService.DeleteVendor(vendor);

            //activity log
            _customerActivityService.InsertActivity("DeleteVendor", _localizationService.GetResource("ActivityLog.DeleteVendor"), vendor.Id);

            SuccessNotification(_localizationService.GetResource("Admin.Vendors.Deleted"));
            return RedirectToAction("List");
        }

        #endregion        
    }
}