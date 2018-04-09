using System.Threading.Tasks;
using Gico.FrontEndModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEnd.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(ILogger<ProductController> logger) : base(logger)
        {
        }
        public async Task<IActionResult> Index()
        {
            ProductDetailViewModel model = new ProductDetailViewModel()
            {
                Name = "Name",
                Id = "Id",
                Code = "Code",
                Quantity = 1000,

            };
            return View(model);
        }
    }
}