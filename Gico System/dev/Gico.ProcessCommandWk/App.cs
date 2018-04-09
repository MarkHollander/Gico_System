using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gico.CQRS.Service.Interfaces;

namespace Gico.ProcessCommandWk
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IMessageProcessor _messageProcessor;

        public App(ILogger<App> logger, IMessageProcessor messageProcessor)
        {
            _logger = logger;
            _messageProcessor = messageProcessor;
        }

        private void RegisterCommandHandler(ServiceProvider provider)
        {
            try
            {
                _messageProcessor.Register(provider);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }

        }
        public void Run(ServiceProvider provider)
        {
            try
            {
                RegisterCommandHandler(provider);
                _messageProcessor.Start();
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