using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Worker.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cynosura.Template.Worker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorker(this IServiceCollection services)
        {
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            services.AddSingleton<IHostedService, MainWorker>();
            services.AddTransient<CoreLogProvider>();
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            return services;
        }
    }
}
