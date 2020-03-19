using Autofac;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Worker.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace Cynosura.Template.Worker.Autofac
{
    class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserInfoProvider>().As<IUserInfoProvider>().InstancePerLifetimeScope();
            builder.RegisterType<MainWorker>().As<IHostedService>().SingleInstance();
            builder.RegisterType<CoreLogProvider>();
        }
    }
}
