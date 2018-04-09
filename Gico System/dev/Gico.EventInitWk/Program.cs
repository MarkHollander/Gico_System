using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Gico.AddressDataObject.Implements;
using Gico.AddressDataObject.Interfaces;
using Gico.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.EventStorage.Implements;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemAppService.Implements;
using Gico.SystemAppService.Interfaces;
using Gico.SystemEventsHandler;
using Gico.SystemCacheStorage.Implements;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemDataObject.Implements;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;
using Polly;
using Gico.Resilience.Http;
using Gico.EsStorage;

namespace Gico.EventInitWk
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            ConfigurationRoot configurationRoot = (ConfigurationRoot)configuration;
            var configurationProvider = configurationRoot.Providers.Where(p => p.GetType() == typeof(JsonConfigurationProvider))
                .FirstOrDefault(p => ((JsonConfigurationProvider)p).Source.Path == "appsettings.json");

            ConfigSetting.Init(configurationProvider);

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<App>().Run(serviceProvider).Wait();

            Console.WriteLine("App Start");
            var t = Console.ReadLine();
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging();
            // add services
            serviceCollection.AddTransient<IInitEventAppService, InitEventAppService>();
            serviceCollection.AddTransient<ILocationAppService, LocationAppService>();

            serviceCollection.AddTransient<ILanguageService, LanguageService>();
            serviceCollection.AddTransient<ILocationService, LocationService>();

            serviceCollection.AddTransient<ILanguageRepository, LanguageRepository>();
            serviceCollection.AddTransient<ILocationRepository, LocationRepository>();

            serviceCollection.AddTransient<IEsStorage, EsStorage.EsStorage>();
            serviceCollection.AddTransient<ILanguageCacheStorage, LanguageCacheStorage>();
            serviceCollection.AddTransient<IRedisStorage, RedisStorage>();

            serviceCollection.AddSingleton<IResilientHttpClientFactory, ResilientHttpClientFactory>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilientHttpClient>>();
                //var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var retryCount = 3;
                var exceptionsAllowedBeforeBreaking = 5;
                return new ResilientHttpClientFactory(logger, exceptionsAllowedBeforeBreaking, retryCount);
            });
            serviceCollection.AddSingleton<IHttpClient, ResilientHttpClient>(sp => sp.GetService<IResilientHttpClientFactory>().CreateResilientHttpClient());

            serviceCollection.AddTransient<IEventSender, EventSender>();
            serviceCollection.AddSingleton<IEventBus, EventBusRabbitMq>();
            serviceCollection.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = ConfigSettingEnum.RabitMqHost.GetConfig(),
                    UserName = ConfigSettingEnum.RabitMqUserName.GetConfig(),
                    Password = ConfigSettingEnum.RabitMqPassword.GetConfig()
                };
                return new DefaultRabbitMqPersistentConnection(factory, logger);
            });
            //// add app
            serviceCollection.AddTransient<App>();
        }



    }
}
