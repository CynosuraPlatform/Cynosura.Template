﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Security;
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
            return services;
        }
    }
}
