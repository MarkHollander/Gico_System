using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Bus.Implements.RabitMq;
using Gico.CQRS.Bus.Interfaces;
using Gico.CQRS.Bus.Interfaces.RabitMq;
using Gico.CQRS.Service.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.FileAppService.Interfaces;
using Gico.FileService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Gico.Cdn
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
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = Int64.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());

            services.AddLogging();

            services.AddTransient<IFileAppService, FileAppService.Implements.FileAppService>();

            services.AddTransient<IFileService, FileService.Implements.FileService>();

            services.AddTransient<ICommandSender, CommandSender>();

            services.AddSingleton<ICommandBus, CommandBusRabbitMq>();

            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = ConfigSettingEnum.RabitMqHost.GetConfig(),
                    UserName = ConfigSettingEnum.RabitMqUserName.GetConfig(),
                    Password = ConfigSettingEnum.RabitMqPassword.GetConfig(),

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
            }
            app.UseStaticFiles();

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
