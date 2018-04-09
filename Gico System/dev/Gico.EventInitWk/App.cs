using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gico.SystemAppService.Interfaces;

namespace Gico.EventInitWk
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IInitEventAppService _initEventAppService;
        private readonly ILocationAppService _locationAppService;

        public App(ILogger<App> logger, IInitEventAppService initEventAppService, ILocationAppService locationAppService)
        {
            _logger = logger;
            _initEventAppService = initEventAppService;
            _locationAppService = locationAppService;
        }

        public async Task Run(ServiceProvider provider)
        {
            try
            {

                //await _initEventAppService.LanguageInit();
                //await _locationAppService.InitEvent();

                _logger.LogInformation("This is a console application for ");
                System.Console.ReadKey();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                _logger.LogError(e, "ProcessCommand Run Exception ");
            }

        }
    }
}