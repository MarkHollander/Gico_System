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
    public class CartController : BaseController
    {
        private readonly ICartAppService _cartAppService;

        public CartController(ICartAppService cartAppService, ILogger<CartController> logger) : base(logger)
        {
            _cartAppService = cartAppService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _cartAppService.Get();
            return PartialView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CartItemChangeViewModel model)
        {
            model.ProductId = "ProductId";
            var response = await _cartAppService.Change(model);
            return Json(response);
        }



    }
}