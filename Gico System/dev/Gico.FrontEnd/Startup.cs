using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Gico.AddressDataObject.Implements;
using Gico.AddressDataObject.Interfaces;
using Gico.AppService;
using Gico.Caching.Abstractions;
using Gico.Caching.Redis;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.DataObject;
using Gico.EmailOrSmsDataObject.Implements;
using Gico.EmailOrSmsDataObject.Interfaces;
using Gico.EmailOrSmsService.Implements;
using Gico.EmailOrSmsService.Interfaces;
using Gico.EsStorage;
using Gico.FrontEnd.Validations;
using Gico.FrontEndAppService.Implements;
using Gico.FrontEndAppService.Interfaces;
using Gico.FrontEndModels.Models;
using Gico.MarketingCacheStorage.Implements;
using Gico.MarketingCacheStorage.Interfaces;
using Gico.MarketingDataObject.Implements;
using Gico.MarketingDataObject.Implements.PageBuilder;
using Gico.MarketingDataObject.Interfaces;
using Gico.MarketingDataObject.Interfaces.PageBuilder;
using Gico.OrderCacheStorage.Implements;
using Gico.OrderCacheStorage.Interfaces;
using Gico.OrderDataObject.Implements;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderService.Implements;
using Gico.OrderService.Interfaces;
using Gico.Resilience.Http;
using Gico.ShardingDataObject.Implements;
using Gico.ShardingDataObject.Interfaces;
using Gico.SystemCacheStorage.Implements;
using Gico.SystemCacheStorage.Interfaces;
using Gico.SystemDataObject.Implements;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemService.Implements;
using Gico.SystemService.Implements.PageBuilder;
using Gico.SystemService.Interfaces;
using Gico.SystemService.Interfaces.PageBuilder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Serilog;
using StackExchange.Redis;

namespace Gico.FrontEnd
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
            services.AddSingleton(new LoggerFactory()
                 .AddConsole()
                 .AddDebug());
            services.AddLogging();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie = new CookieBuilder()
                    {
                        Name = SystemDefine.AuthenCookieName,
                        HttpOnly = true,
                        //Domain = ".gico.vn",
                    };
                    if (!string.IsNullOrEmpty(ConfigSettingEnum.CookieDomain.GetConfig()))
                    {
                        options.Cookie.Domain = ConfigSettingEnum.CookieDomain.GetConfig();
                    }
                    //options.TicketDataFormat =new 

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
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig())),

                    };
                })
                .AddFacebook(options =>
                {
                    options.AppId = ConfigSettingEnum.AuthFacebookAppid.GetConfig();
                    options.AppSecret = ConfigSettingEnum.AuthFacebookAppsecret.GetConfig();
                    options.Scope.Add("email");
                    options.Fields.Add("name");
                    options.Fields.Add("email");
                    options.SaveTokens = true;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = ConfigSettingEnum.AuthGoogleClientid.GetConfig();
                    options.ClientSecret = ConfigSettingEnum.AuthGoogleClientsecret.GetConfig();
                    options.SaveTokens = true;
                    options.Events = new OAuthEvents()
                    {
                        OnRemoteFailure = ctx =>
                        {
                            ctx.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(ctx.Failure.Message));
                            ctx.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                    options.ClaimActions.MapJsonSubKey("urn:google:image", "image", "url");
                    options.ClaimActions.Remove(ClaimTypes.GivenName);
                });
            services.AddDataProtection().SetApplicationName(SystemDefine.DataProtectionRedisKey)
                .PersistKeysToRedis(RedisConnection.Current.GetWriteConnection, SystemDefine.DataProtectionRedisKey);
            //.UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            //{
            //    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            //    ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
            //}
            // );

            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IHomeAppService, HomeAppService>();
            services.AddTransient<ICustomerAppService, CustomerAppService>();
            services.AddTransient<ICartAppService, CartAppService>();
            services.AddTransient<ICheckoutAppService, CheckoutAppService>();

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<IEmailSmsService, EmailSmsService>();

            services.AddTransient<IShardingConfigRepository, ShardingConfigRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<ICartItemDetailRepository, CartItemDetailRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IActionDefineRepository, ActionDefineRepository>();
            services.AddTransient<IRoleActionMappingRepository, RoleActionMappingRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITemplateRepository, TemplateRepository>();
            services.AddTransient<ITemplateConfigRepository, TemplateConfigRepository>();
            services.AddTransient<IEmailSmsRepository, EmailSmsRepository>();
            services.AddTransient<IVerifyRepository, VerifyRepository>();
            services.AddTransient<ICustomerExternalLoginRepository, CustomerExternalLoginRepository>();

            services.AddTransient<ICommandSender, CommandSender>();
            services.AddTransient<IEventSender, EventSender>();

            services.AddTransient<ICartCacheStorage, CartCacheStorage>();
            services.AddTransient<IMenuCacheStorage, MenuCacheStorage>();
            services.AddTransient<ILocaleStringResourceCacheStorage, LocaleStringResourceCacheStorage>();
            services.AddTransient<ICustomerCacheStorage, CustomerCacheStorage>();
            services.AddTransient<IRoleCacheStorage, RoleCacheStorage>();
            services.AddTransient<ITemplateCacheStorage, TemplateCacheStorage>();

            services.AddTransient<IRedisStorage, RedisStorage>();
            services.AddTransient<IDistributedCache, RedisCache>();

            services.AddTransient<IEsStorage, EsStorage.EsStorage>();

            services.AddSingleton<ICommandBus, CommandBusRabbitMq>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>();

            services.AddSingleton<IResilientHttpClientFactory, ResilientHttpClientFactory>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilientHttpClient>>();
                //var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
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

            services.AddMvc()
                .AddSessionStateTempDataProvider()
                ;

            services.AddSession();
            //validate register
            //services.AddTransient<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .MinimumLevel.Warning()
                .CreateLogger();
            Log.Logger = logger;

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(ConfigureRoute);
            CreateConsumerResultQueue(app.ApplicationServices);
        }
        public void CreateConsumerResultQueue(IServiceProvider provider)
        {
            provider.GetService<ICommandBus>().CreateConsumerResultChannel();
        }
        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //routeBuilder.MapRoute(
            //    name: "home",
            //    template: "{locale}",
            //    defaults: new { controller = "Home", action = "Index" },
            //    constraints: null,
            //    dataTokens: new { locale = "en-US" });
            routeBuilder.MapRoute(
                name: "DecryptCookie",
                template: "DecryptCookie",
                defaults: new { controller = "Account", action = "DecryptCookie" }
            );
            //URL: /vn/login
            routeBuilder.MapRoute(
                name: "login",
                template: "{culture=vi}/login",
                defaults: new { controller = "Account", action = "Login" }
            );
            routeBuilder.MapRoute(
                name: "login_default",
                template: "login",
                defaults: new { controller = "Account", action = "Login" }
            );
            //URL: /vn/SigninFacebook
            routeBuilder.MapRoute(
                name: "ExternalLogin",
                template: "{culture=vi}/ExternalLogin",
                defaults: new { controller = "Account", action = "ExternalLogin" }
            );
            routeBuilder.MapRoute(
                name: "ExternalLogin_default",
                template: "ExternalLogin",
                defaults: new { controller = "Account", action = "ExternalLogin" }
            );
            //URL: / vn / SigninFacebookCallback
            routeBuilder.MapRoute(
                name: "ExternalLoginCallback",
                template: "{culture=vi}/ExternalLoginCallback",
                defaults: new { controller = "Account", action = "ExternalLoginCallback" }
            );
            routeBuilder.MapRoute(
                name: "ExternalLoginCallback_default",
                template: "ExternalLoginCallback",
                defaults: new { controller = "Account", action = "ExternalLoginCallback" }
            );
            routeBuilder.MapRoute(
                name: "VerifyExternalLoginWhenAccountIsExist_default",
                template: "VerifyExternalLoginWhenAccountIsExist",
                defaults: new { controller = "Account", action = "VerifyExternalLoginWhenAccountIsExist" }
            );
            //URL: /vn/logout
            routeBuilder.MapRoute(
                name: "logout",
                template: "{culture=vi}/logout",
                defaults: new { controller = "Account", action = "Logout" }
            );

            //URL: /vn/register
            routeBuilder.MapRoute(
                name: "register",
                template: "{culture=vi}/register",
                defaults: new { controller = "Account", action = "Register" }
            );
            //URL: /vn/register
            routeBuilder.MapRoute(
                name: "registersuccess",
                template: "{culture=vi}/registersuccess",
                defaults: new { controller = "Account", action = "RegisterSuccess" }
            );

            //URL: /vn/iphone-8-AAA1.html
            routeBuilder.MapRoute(
                name: "product_detail",
                template: $"{{culture=vi}}/{{name}}-{{{SystemDefine.ParamProductcode}}}.html",
                defaults: new { controller = "Product", action = "Index" }
                );

            //URL: /vn/cart/add
            routeBuilder.MapRoute(
                name: "cart_add",
                template: "{culture=vi}/cart/add",
                defaults: new { controller = "Cart", action = "Add" }
            );
            //URL: /vn/cart/
            routeBuilder.MapRoute(
                name: "cart",
                template: "{culture=vi}/cart",
                defaults: new { controller = "Cart", action = "Index" }
            );
            //URL: /vn/checkout
            routeBuilder.MapRoute(
                name: "checkout",
                template: "{culture=vi}/checkout",
                defaults: new { controller = "Checkout", action = "Index" }
            );

            //URL: /vn/checkout
            routeBuilder.MapRoute(
                name: "checkout_locationsearch",
                template: "{culture=vi}/checkout/locationsearch/{q}",
                defaults: new { controller = "Checkout", action = "LocationSearch" }
            );

            //URL: /vn/slug
            //routeBuilder.MapRoute(
            //    name: "listing",
            //    template: $"{{culture=vi}}/{{{StringDefine.ParamSlugInListingPage}}}",
            //    defaults: new { controller = "Listing", action = "Index" }
            //);

            //URL: /vn/cart
            //routeBuilder.MapRoute(
            //    name: "cart",
            //    template: "{{culture=vi}}/cart",
            //    defaults: new { controller = "Cart", action = "Index" }
            //);
            ////URL: /vn
            //routeBuilder.MapRoute(
            //    name: "home",
            //    template: $"{{culture=vi}}",
            //    defaults: new { controller = "Home", action = "Index" }
            //);
            routeBuilder.MapRoute(
                name: "home_default",
                template: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routeBuilder.MapRoute(
                name: "default",
                template: "{culture=vi}/{controller=Home}/{action=Index}/{id?}");

        }
    }
}
