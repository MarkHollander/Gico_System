using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Gico.Config;
using Microsoft.AspNetCore.Mvc;
using Gico.FrontEnd.Models;
using Gico.FrontEndAppService.Filters;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndModels.Models;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEnd.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeAppService _homeAppService;
        private readonly string _templateCode;

        public HomeController(ILogger<HomeController> logger, IHomeAppService homeAppService) : base(logger)
        {
            _homeAppService = homeAppService;
            _templateCode = ConfigSettingEnum.TemplateDefault.GetConfig();
        }

        [OutputCache(100)]
        public async Task<IActionResult> Index()
        {
            try
            {
                HomeViewModel model = await _homeAppService.Get(_templateCode);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, Common.Common.GetMethodName());
                throw;
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
