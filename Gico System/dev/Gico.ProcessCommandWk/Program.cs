using System;
using System.IO;
using System.Linq;
using Gico.AddressDataObject.Implements;
using Gico.AddressDataObject.Interfaces;
using Gico.Caching.Redis;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.EventStorage.Implements;
using Gico.CQRS.EventStorage.Interfaces;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.EmailOrSmsDataObject.Implements;
using Gico.EmailOrSmsService.Implements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Gico.FileCommandsHandler;
using Gico.FileDataObject.Implements;
using Gico.FileDataObject.Interfaces;
using Gico.OrderCacheStorage.Implements;
using Gico.OrderCacheStorage.Interfaces;
using Gico.SystemCommandsHandler;
using Gico.SystemDataObject.Implements;
using Gico.SystemDataObject.Interfaces;
using Gico.OrderCommandsHandler;
using Gico.OrderDataObject.Implements;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderService.Implements;
using Gico.OrderService.Interfaces;
using Gico.ShardingConfigService.Implements;
using Gico.ShardingConfigService.Interfaces;
using Gico.ShardingDataObject.Implements;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemCacheStorage.Implements;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;
using Gico.SystemCommandsHandler.PageBuilder;
using Gico.SystemService.Implements.PageBuilder;
using Gico.SystemService.Interfaces.PageBuilder;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.MarketingDataObject.Implements.PageBuilder;
using Gico.SystemService.Interfaces.Banner;
using Gico.MarketingDataObject.Implements.Banner;
using Gico.SystemService.Implements.Banner;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.SystemCommandsHandler.Banner;
using Gico.EmailOrSmsService.Interfaces;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsCommandsHandler;
using Gico.MarketingCacheStorage.Implements;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.MarketingDataObject.Implements;
using Gico.MarketingDataObject.Implements.ProductGroup;
using Gico.MarketingDataObject.Interfaces;
using Gico.MarketingDataObject.Interfaces.ProductGroup;
using Gico.Razor;
using Gico.SendMail;
using Gico.SystemCacheStorage.Implements.Product;
using Gico.SystemCacheStorage.Interfaces.Product;
using Gico.SystemService.Implements.Product;
using Gico.SystemService.Implements.ProductGroup;
using Gico.SystemService.Interfaces.Product;
using Gico.SystemService.Interfaces.ProductGroup;
using RazorLight;

namespace Gico.ProcessCommandWk
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
            //serviceCollection.AddTransient<IMessageHandler, UserCommandHandler>();
            //serviceCollection.AddTransient<UserCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, FileCommandHandler>();
            serviceCollection.AddTransient<FileCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, MenuCommandHandler>();
            serviceCollection.AddTransient<MenuCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, CartCommandHandler>();
            serviceCollection.AddTransient<CartCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, CustomerCommandHandler>();
            serviceCollection.AddTransient<CustomerCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, RoleCommandHandler>();
            serviceCollection.AddTransient<RoleCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, LanguageCommandHandler>();
            serviceCollection.AddTransient<LanguageCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, LocaleStringResourceCommandHandler>();
            serviceCollection.AddTransient<LocaleStringResourceCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, MeasureUnitCommandHandler>();
            serviceCollection.AddTransient<MeasureUnitCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, CategoryCommandHandler>();
            serviceCollection.AddTransient<CategoryCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, VendorCommandHandler>();
            serviceCollection.AddTransient<VendorCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, EmailOrSmsCommandHandler>();
            serviceCollection.AddTransient<EmailOrSmsCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, ProductAttributeCommandHandler>();
            serviceCollection.AddTransient<ProductAttributeCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, ProductAttributeValueCommandHandler>();
            serviceCollection.AddTransient<ProductAttributeValueCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, AttrCategoryCommandHandler>();
            serviceCollection.AddTransient<AttrCategoryCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, Category_VariationTheme_MappingHandler>();
            serviceCollection.AddTransient<Category_VariationTheme_MappingHandler>();
            serviceCollection.AddTransient<IMessageHandler, ManufacturerCategoryMappingCommandHandler>();
            serviceCollection.AddTransient<ManufacturerCategoryMappingCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, ManufacturerCommandHandler>();
            serviceCollection.AddTransient<ManufacturerCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, ManufacturerCategoryMappingCommandHandler>();
            serviceCollection.AddTransient<ManufacturerCategoryMappingCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, ProductGroupCommandHandler>();
            serviceCollection.AddTransient<ProductGroupCommandHandler>();
            serviceCollection.AddTransient<IMessageHandler, LocationCommandHandler>();
            serviceCollection.AddTransient<LocationCommandHandler>();

            serviceCollection.AddTransient<IShardingService, ShardingService>();
            serviceCollection.AddTransient<ICartService, CartService>();
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<ICommonService, CommonService>();
            serviceCollection.AddTransient<IRoleService, RoleService>();
            serviceCollection.AddTransient<ILanguageService, LanguageService>();
            serviceCollection.AddTransient<ILocaleStringResourceService, LocaleStringResourceService>();
            serviceCollection.AddTransient<IMeasureUnitService, MeasureUnitService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();
            serviceCollection.AddTransient<IVendorService, VendorService>();
            serviceCollection.AddTransient<IEmailSmsService, EmailSmsService>();
            serviceCollection.AddTransient<IProductAttributeService, ProductAttributeService>();
            serviceCollection.AddTransient<IProductAttributeValueService, ProductAttributeValueService>();
            serviceCollection.AddTransient<IAttrCategoryService, AttrCategoryService>();
            serviceCollection.AddTransient<IVariationThemeService, VariationThemeService>();
            serviceCollection.AddTransient<IManufacturerCategoryMappingService, ManufacturerCategoryMappingService>();
            serviceCollection.AddTransient<IMenuService, MenuService>();
            serviceCollection.AddTransient<IProductGroupService, ProductGroupService>();
            serviceCollection.AddTransient<ILocationService, LocationService>();
            serviceCollection.AddTransient<IManufacturerService, ManufacturerService>();
            serviceCollection.AddTransient<IProductService, ProductService>();

            serviceCollection.AddTransient<IFileRepository, FileRepository>();
            serviceCollection.AddTransient<IMenuRepository, MenuRepository>();
            serviceCollection.AddTransient<ICartRepository, CartRepository>();
            serviceCollection.AddTransient<ICartItemRepository, CartItemRepository>();
            serviceCollection.AddTransient<ICartItemDetailRepository, CartItemDetailRepository>();
            serviceCollection.AddTransient<IShardingConfigRepository, ShardingConfigRepository>();
            serviceCollection.AddTransient<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddTransient<ICommonRepository, CommonRepository>();
            serviceCollection.AddTransient<IActionDefineRepository, ActionDefineRepository>();
            serviceCollection.AddTransient<IRoleActionMappingRepository, RoleActionMappingRepository>(); 
            serviceCollection.AddTransient<IDepartmentRepository, DepartmentRepository>();
            serviceCollection.AddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.AddTransient<ILanguageRepository, LanguageRepository>();
            serviceCollection.AddTransient<ILocaleStringResourceRepository, LocaleStringResourceRepository>();
            serviceCollection.AddTransient<IMeasureUnitRepository, MeasureUnitRepository>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddTransient<IVendorRepository, VendorRepository>();
            serviceCollection.AddTransient<IEmailSmsRepository, EmailSmsRepository>();
            serviceCollection.AddTransient<IVerifyRepository, VerifyRepository>();
            serviceCollection.AddTransient<ICustomerExternalLoginRepository, CustomerExternalLoginRepository>();
            serviceCollection.AddTransient<IProductAttributeRepository, ProductAttributeRepository>();
            serviceCollection.AddTransient<IProductAttributeValueRepository, ProductAttributeValueRepository>();
            serviceCollection.AddTransient<IProductGroupRepository, ProductGroupRepository>();
            serviceCollection.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            serviceCollection.AddTransient<ILocationRepository, LocationRepository>();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();

            serviceCollection.AddTransient<ISendMailClient, SendMailClientByAws>();
            serviceCollection.AddTransient<ITemplateRenderer, RazorRenderer>();
            serviceCollection.AddTransient<IAttrCategoryRepository, AttrCategoryRepository>();
            serviceCollection.AddTransient<IVariationThemeRepository, VariationThemeRepository>();
            serviceCollection.AddTransient<IManufacturerCategoryMappingRepository, ManufacturerCategoryMappingRepository>();

            serviceCollection.AddTransient<IRedisStorage, RedisStorage>();
            serviceCollection.AddTransient<ICartCacheStorage, CartCacheStorage>();
            serviceCollection.AddTransient<ICustomerCacheStorage, CustomerCacheStorage>();
            serviceCollection.AddTransient<IRoleCacheStorage, RoleCacheStorage>();
            serviceCollection.AddTransient<ILanguageCacheStorage, LanguageCacheStorage>();
            serviceCollection.AddTransient<ILocaleStringResourceCacheStorage, LocaleStringResourceCacheStorage>();
            serviceCollection.AddTransient<IComponentCacheStorage, ComponentCacheStorage>();
            serviceCollection.AddTransient<IMenuCacheStorage, MenuCacheStorage>();
            serviceCollection.AddTransient<IProductCacheStorage, ProductCacheStorage>();

            #region Page Builder
            serviceCollection.AddTransient<ITemplateService, TemplateService>();
            serviceCollection.AddTransient<ITemplateRepository, TemplateRepository>();
            serviceCollection.AddTransient<ITemplateCacheStorage, TemplateCacheStorage>();            
            serviceCollection.AddTransient<IMessageHandler, TemplateCommandHandler>();
            serviceCollection.AddTransient<TemplateCommandHandler>();

            #region Template Config                        
            serviceCollection.AddSingleton<ITemplateConfigRepository, TemplateConfigRepository>();
            #endregion

            #region Banner            
            serviceCollection.AddSingleton<IBannerService, BannerService>();
            serviceCollection.AddSingleton<IBannerRepository, BannerRepository>();
            serviceCollection.AddTransient<IMessageHandler, BannerCommandHandler>();
            serviceCollection.AddTransient<BannerCommandHandler>();
            #endregion

            #region Banner Item            
            serviceCollection.AddSingleton<IBannerItemRepository, BannerItemRepository>();
            #endregion

            #endregion

            serviceCollection.AddTransient<ICommandSender, CommandSender>();
            serviceCollection.AddTransient<IEventSender, EventSender>();

            serviceCollection.AddTransient<IMessageProcessor, CommandProcessor>();

            serviceCollection.AddTransient<ICommandStorageDao, CommandStorageDao>();
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
