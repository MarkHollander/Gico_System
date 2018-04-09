using System;
using System.Linq;
using System.Text;
using Gico.AppService;
using Gico.Caching.Redis;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.FileStorage;
using Gico.Resilience.Http;
using Gico.ShardingConfigService.Implements;
using Gico.ShardingConfigService.Interfaces;
using Gico.ShardingDataObject.Implements;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemAppService;
using Gico.SystemAppService.Implements;
using Gico.SystemAppService.Interfaces;
using Gico.SystemCacheStorage.Implements;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemDataObject.Implements;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Gico.SystemAppService.Interfaces.PageBuilder;
using Gico.SystemService.Interfaces.PageBuilder;
using Gico.SystemService.Implements.PageBuilder;
using Gico.MarketingDataObject.Implements.PageBuilder;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.SystemAppService.Implements.PageBuilder;
using Gico.SystemAppService.Interfaces.Banner;
using Gico.SystemAppService.Implements.Banner;
using Gico.SystemService.Implements.Banner;
using Gico.SystemService.Interfaces.Banner;
using Gico.MarketingDataObject.Interfaces.Banner;
using Gico.MarketingDataObject.Implements.Banner;
using Gico.EmailOrSmsAppService;
using Gico.EmailOrSmsAppService.Interfaces;
using Gico.EmailOrSmsAppService.Implements;
using Gico.EmailOrSmsService.Implements;
using Gico.EmailOrSmsService.Interfaces;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsDataObject.Implements;
using Gico.MarketingCacheStorage.Implements;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.MarketingDataObject.Implements;
using Gico.MarketingDataObject.Interfaces;
using Gico.AddressDataObject.Interfaces;
using Gico.AddressDataObject.Implements;
using Gico.MarketingDataObject.Implements.ProductGroup;
using Gico.MarketingDataObject.Interfaces.ProductGroup;
using Gico.SystemAppService.Implements.ProductGroup;
using Gico.SystemAppService.Interfaces.ProductGroup;
using Gico.SystemService.Implements.ProductGroup;
using Gico.SystemService.Interfaces.ProductGroup;
using Gico.SystemAppService.Interfaces.Warehouse;
using Gico.SystemService.Interfaces.Warehouse;
using Gico.SystemService.Implements.Warehouse;
using Gico.SystemAppService.Implements.Warehouse;
using Gico.SystemCacheStorage.Implements.Product;
using Gico.SystemCacheStorage.Interfaces.Product;
using Gico.SystemService.Implements.Product;
using Gico.SystemService.Interfaces.Product;

namespace Gico.Cms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationRoot configurationRoot = (ConfigurationRoot)configuration;
            var configurationProvider = configurationRoot.Providers.Where(p => p.GetType() == typeof(JsonConfigurationProvider))
                .FirstOrDefault(p => ((JsonConfigurationProvider)p).Source.Path == "appsettings.json");

            ConfigSetting.Init(configurationProvider);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            services.AddLogging();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie = new CookieBuilder()
                    {
                        Name = "gicoOAU",
                        HttpOnly = true,

                    };
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.Audience = ConfigSettingEnum.JwtTokensAudience.GetConfig();
                    cfg.Authority = ConfigSettingEnum.JwtTokensAuthority.GetConfig();
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = ConfigSettingEnum.JwtTokensIssuer.GetConfig(),
                        ValidAudience = ConfigSettingEnum.JwtTokensAudience.GetConfig(),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig())),

                    };
                });

            services.AddDataProtection().SetApplicationName(SystemDefine.DataProtectionRedisKey)
                .PersistKeysToRedis(RedisConnection.Current.GetWriteConnection, SystemDefine.DataProtectionRedisKey);
            // Add application services.
            services.AddTransient<IAccountAppService, AccountAppService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<IShardingAppService, ShardingAppService>();
            services.AddTransient<IMenuAppService, MenuAppService>();
            services.AddTransient<IRoleAppService, RoleAppService>();
            services.AddTransient<ILanguageAppService, LanguageAppService>();
            services.AddTransient<IFileAppService, FileAppService>();
            services.AddTransient<ICategoryAppService, CategoryAppService>();
            services.AddTransient<ILocaleStringResourceAppService, LocaleStringResourceAppService>();
            services.AddTransient<IProductAttributeAppService, ProductAttributeAppService>();
            services.AddTransient<IAttrCategoryAppService,AttrCategoryAppService>();
            services.AddTransient<IVariationThemeAppService, VariationThemeAppService>();
            services.AddTransient<IManufacturerCategoryMappingAppService, ManufacturerCategoryMappingAppService>();
            services.AddTransient<IVendorAppService, VendorAppService>();
            services.AddTransient<IMeasureUnitAppService, MeasureUnitAppService>();
            services.AddTransient<IEmailSmsAppService, EmailSmsAppService>();
            services.AddTransient<IProductGroupAppService, ProductGroupAppService>();
            services.AddTransient<IWarehouseAppService, WarehouseAppService>();

            services.AddTransient<ICurrentContext, CurrentContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IShardingService, ShardingService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ILocaleStringResourceService, LocaleStringResourceService>();
            services.AddTransient<IMeasureUnitService, MeasureUnitService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<IEmailSmsService, EmailSmsService>();
            services.AddTransient<IProductAttributeService, ProductAttributeService>();
            services.AddTransient<IAttrCategoryService, AttrCategoryService>();
            services.AddTransient<IVariationThemeService, VariationThemeService>();
            services.AddTransient<IManufacturerCategoryMappingService, ManufacturerCategoryMappingService>();
            services.AddTransient<IProductGroupService, ProductGroupService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ICategoryService, CategoryService>();
            //services.AddSingleton<IUserRepositoryRead, UserRepositoryRead>();
            //services.AddSingleton<IShardingConfigDao, ShardingConfigDao>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IShardingConfigRepository, ShardingConfigRepository>();
            services.AddTransient<IActionDefineRepository, ActionDefineRepository>();
            services.AddTransient<IRoleActionMappingRepository, RoleActionMappingRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ILocaleStringResourceRepository, LocaleStringResourceRepository>();
            services.AddTransient<IMeasureUnitRepository, MeasureUnitRepository>();
            services.AddTransient<IEmailSmsRepository, EmailSmsRepository>();
            services.AddTransient<IVerifyRepository, VerifyRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();
            services.AddTransient<ICustomerExternalLoginRepository, CustomerExternalLoginRepository>();
            services.AddTransient<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddTransient<IAttrCategoryRepository, AttrCategoryRepository>();
            services.AddTransient<IVariationThemeRepository, VariationThemeRepository>();
            services.AddTransient<IManufacturerCategoryMappingRepository, ManufacturerCategoryMappingRepository>();
            services.AddTransient<IProductGroupRepository, ProductGroupRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IMenuCacheStorage, MenuCacheStorage>();
            services.AddTransient<ILanguageCacheStorage, LanguageCacheStorage>();
            services.AddTransient<ICustomerCacheStorage, CustomerCacheStorage>();
            services.AddTransient<IRoleCacheStorage, RoleCacheStorage>();
            services.AddTransient<ILocaleStringResourceCacheStorage, LocaleStringResourceCacheStorage>();
            services.AddTransient<IComponentCacheStorage, ComponentCacheStorage>();
            services.AddTransient<IProductCacheStorage, ProductCacheStorage>();

            services.AddTransient<IRedisStorage, RedisStorage>();
            services.AddTransient<IFileStorage, FileStorage.FileStorage>();

            services.AddTransient<ICommandSender, CommandSender>();
            services.AddTransient<IEventSender, EventSender>();

            services.AddTransient<IManufacturerAppService, ManufacturerAppService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();

            services.AddSingleton<ICommandBus, CommandBusRabbitMq>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>();

            #region Template
            services.AddSingleton<ITemplateAppService, TemplateAppService>();
            services.AddSingleton<ITemplateService, TemplateService>();
            services.AddSingleton<ITemplateRepository, TemplateRepository>();
            services.AddSingleton<ITemplateCacheStorage, TemplateCacheStorage>();
            #endregion

            #region Template Config
            services.AddSingleton<ITemplateConfigAppService, TemplateConfigAppService>();            
            services.AddSingleton<ITemplateConfigRepository, TemplateConfigRepository>();
            #endregion

            #region Banner
            services.AddSingleton<IBannerAppService, BannerAppService>();
            services.AddSingleton<IBannerService, BannerService>();
            services.AddSingleton<IBannerRepository, BannerRepository>();
            #endregion

            #region Banner Item            
            services.AddSingleton<IBannerItemRepository, BannerItemRepository>();
            #endregion

            #region ProductAttribute

            services.AddSingleton<IProductAttributeAppService, ProductAttributeAppService>();
            services.AddSingleton<IProductAttributeService, ProductAttributeService>();
            services.AddSingleton<IProductAttributeRepository, ProductAttributeRepository>();

            services.AddSingleton<IProductAttributeValueAppService, ProductAttributeValueAppService>();
            services.AddSingleton<IProductAttributeValueService, ProductAttributeValueService>();
            services.AddSingleton<IProductAttributeValueRepository, ProductAttributeValueRepository>();

            #endregion

            #region Location

            services.AddSingleton<ILocationAppService, LocationAppService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<ILocationRepository, LocationRepository>();

            #endregion

            services.AddSingleton<IResilientHttpClientFactory, ResilientHttpClientFactory>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilientHttpClient>>();
                var retryCount = 3;
                var exceptionsAllowedBeforeBreaking = 5;
                return new ResilientHttpClientFactory(logger, exceptionsAllowedBeforeBreaking, retryCount);
            });
            services.AddSingleton<IHttpClient, ResilientHttpClient>(sp => sp.GetService<IResilientHttpClientFactory>().CreateResilientHttpClient());

            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
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

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole(LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMvc();

            CreateConsumerResultQueue(app.ApplicationServices);
        }
        public void CreateConsumerResultQueue(IServiceProvider provider)
        {
            provider.GetService<ICommandBus>().CreateConsumerResultChannel();
        }
    }
}
