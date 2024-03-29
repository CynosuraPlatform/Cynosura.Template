﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Infrastructure.Messaging;
using Cynosura.Template.Worker.Infrastructure;
using Cynosura.Template.Worker.Jobs;
using Cynosura.Template.Worker.WorkerInfos;

namespace Cynosura.Template.Worker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<RunWorkerInfoJob>(new JobKey(RunWorkerInfoJob.JobKey), o => o.StoreDurably());
            });
            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            services.AddFromConfiguration(configuration, assemblies);
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
            services.AddTransient<WorkerInfoSheduler>();
            services.AddTransient<IHostedService, WorkerInfoShedulerService>();
            services.AddTransient<StartWorkerRunJob>();
            return services;
        }
    }
}
