﻿using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using Cynosura.EF;
using Microsoft.Extensions.Configuration;
using Cynosura.Template.Core.Autofac;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Data;

namespace Cynosura.Template.Worker.Infrastructure
{
    public class AutofacConfig
    {
        public static void ConfigureAutofac(ContainerBuilder builder, IConfiguration configuration)
        {
            var assemblies = GetPlatformAndAppAssemblies();
            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
                .WithParameter((p, c) => p.Name == "connectionString",
                    (p, c) => configuration.GetConnectionString("DefaultConnection"))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserInfoProvider>().As<IUserInfoProvider>().InstancePerLifetimeScope();
            builder.Register(c => new MapperConfiguration(cfg => { cfg.AddProfiles(assemblies); }).CreateMapper())
                .As<IMapper>().SingleInstance();
        }

        private static Assembly[] GetPlatformAndAppAssemblies()
        {
            // hack
            new CoreModule();
            var platformAndAppNames = new[] { "Cynosura", "Cynosura.Template" };
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => platformAndAppNames.Any(n => a.FullName.Contains(n)) ||
                            a.GetReferencedAssemblies()
                                .Any(ra => platformAndAppNames.Any(n => ra.FullName.Contains(n))))
                .ToArray();
        }
    }
}
