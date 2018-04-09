using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gico.AppService;
using Gico.Caching.Redis;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.FileStorage;
using Gico.OmsAppService.Implements;
using Gico.OmsAppService.Interfaces;
using Gico.OrderDataObject.Implements;
using Gico.OrderDataObject.Interfaces;
using Gico.OrderService.Implements;
using Gico.OrderService.Interfaces;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

namespace Gico.Oms
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

            services.AddTransient<IAccountAppService, AccountAppService>();
            services.AddTransient<IGiftcodeAppService, GiftcodeAppService>();

            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IShardingService, ShardingService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IGiftcodeService, GiftcodeService>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IShardingConfigRepository, ShardingConfigRepository>();
            services.AddTransient<IActionDefineRepository, ActionDefineRepository>();
            services.AddTransient<IRoleActionMappingRepository, RoleActionMappingRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IGiftcodeCampaignRepository, GiftcodeCampaignRepository>();

            services.AddTransient<ICustomerCacheStorage, CustomerCacheStorage>();
            services.AddTransient<IRoleCacheStorage, RoleCacheStorage>();

            services.AddTransient<IRedisStorage, RedisStorage>();
            services.AddTransient<IFileStorage, FileStorage.FileStorage>();

            services.AddTransient<ICommandSender, CommandSender>();
            services.AddTransient<IEventSender, EventSender>();

            services.AddSingleton<ICommandBus, CommandBusRabbitMq>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>();

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
                    builder => builder.WithOrigins(ConfigSettingEnum.CorsPolicyDomains.GetConfig().Split(','))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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
