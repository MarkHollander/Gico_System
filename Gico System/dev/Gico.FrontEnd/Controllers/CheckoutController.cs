using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEnd.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly ICheckoutAppService _checkoutAppService;

        public CheckoutController(ICheckoutAppService checkoutAppService, ILogger<CheckoutController> logger) : base(logger)
        {
            _checkoutAppService = checkoutAppService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _checkoutAppService.Checkout();
            return View(model);
        }

        public async Task<IActionResult> LocationSearch()
        {
            var model = await _checkoutAppService.LocationSearch();
            return Json(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressSelected(AddressSelectedViewModel model)
        {
            var response = await _checkoutAppService.AddressSelected(model);
            return Json(response);
        }

        public IActionResult Payment()
        {

            return View();
        }
    }
}