using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.EventStorage.Implements;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCacheStorage.Implements;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemEventsHandler;
using RabbitMQ.Client;
using Gico.Caching.Redis;
using Gico.MarketingCacheStorage.Implements;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.MarketingDataObject.Implements.Banner;
using Gico.MarketingDataObject.Implements.PageBuilder;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.OrderCacheStorage.Implements;
using Gico.OrderCacheStorage.Interfaces;
using Gico.OrderEventsHandler;
using Gico.SystemDataObject.Implements;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemEventsHandler.PageBuilder;
using Gico.SystemService.Implements;
using Gico.SystemService.Implements.Banner;
using Gico.SystemService.Implements.PageBuilder;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.Banner;
using Gico.SystemService.Interfaces.PageBuilder;

namespace Gico.ProcessEventWk
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
            serviceProvider.GetService<App>().Run(serviceProvider);

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
            serviceCollection.AddTransient<IMessageHandler, CurrencyEventHandler>();
            serviceCollection.AddTransient<CurrencyEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, LanguageEventHandler>();
            serviceCollection.AddTransient<LanguageEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, LocaleStringResourceEventHandler>();
            serviceCollection.AddTransient<LocaleStringResourceEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, MenuEventHandler>();
            serviceCollection.AddTransient<MenuEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, CartEventHandler>();
            serviceCollection.AddTransient<CartEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, RoleEventHandler>();
            serviceCollection.AddTransient<RoleEventHandler>();
            serviceCollection.AddTransient<IMessageHandler, TemplateEventHandler>();
            serviceCollection.AddTransient<TemplateEventHandler>();

            serviceCollection.AddTransient<IRoleService, RoleService>();
            serviceCollection.AddTransient<ITemplateService, TemplateService>();
            serviceCollection.AddTransient<IBannerService, BannerService>();
            serviceCollection.AddTransient<IMenuService, MenuService>();

            serviceCollection.AddTransient<IActionDefineRepository, ActionDefineRepository>();
            serviceCollection.AddTransient<IRoleActionMappingRepository, RoleActionMappingRepository>();
            serviceCollection.AddTransient<IDepartmentRepository, DepartmentRepository>();
            serviceCollection.AddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.AddTransient<ITemplateRepository, TemplateRepository>();
            serviceCollection.AddTransient<ITemplateConfigRepository, TemplateConfigRepository>();
            serviceCollection.AddTransient<IBannerRepository, BannerRepository>();
            serviceCollection.AddTransient<IBannerItemRepository, BannerItemRepository>();

            serviceCollection.AddTransient<ICurrencyCacheStorage, CurrencyCacheStorage>();
            serviceCollection.AddTransient<ILanguageCacheStorage, LanguageCacheStorage>();
            serviceCollection.AddTransient<ILocaleStringResourceCacheStorage, LocaleStringResourceCacheStorage>();
            serviceCollection.AddTransient<IMenuCacheStorage, MenuCacheStorage>();
            serviceCollection.AddTransient<ICartCacheStorage, CartCacheStorage>();
            serviceCollection.AddTransient<IRoleCacheStorage, RoleCacheStorage>();
            serviceCollection.AddTransient<ITemplateCacheStorage, TemplateCacheStorage>();
            serviceCollection.AddTransient<IComponentCacheStorage, ComponentCacheStorage>();
            serviceCollection.AddTransient<IMenuCacheStorage, MenuCacheStorage>();

            serviceCollection.AddTransient<IRedisStorage, RedisStorage>();

            serviceCollection.AddTransient<ICommandSender, CommandSender>();

            serviceCollection.AddTransient<IMessageProcessor, EventProcessor>();
            serviceCollection.AddTransient<IEventStorageDao, EventStorageDao>();

            serviceCollection.AddSingleton<ICommandBus, CommandBusRabbitMq>();
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
