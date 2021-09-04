using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cynosura.Messaging;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Infrastructure.Messaging;
using Cynosura.Template.Web.Infrastructure;

namespace Cynosura.Template.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionHandler, ValidationExceptionHandler>();
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            services.AddFromConfiguration(configuration, assemblies);

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<MassTransitServiceOptions>(configuration.GetSection("Messaging"));
            services.AddCynosuraMessaging(null, x =>
            {
                x.AddRabbitMqBus((context, sbc) =>
                {
                    sbc.ConfigureEndpoints(context);
                });
                x.AddConsumers(assemblies);
            });
            services.AddTransient<IHostedService, MessagingWorker>();
            return services;
        }
    }
}
