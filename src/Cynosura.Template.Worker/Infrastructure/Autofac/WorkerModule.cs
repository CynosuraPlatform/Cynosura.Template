using Autofac;
using Cynosura.Template.Core.Security;
using Microsoft.Extensions.Hosting;

namespace Cynosura.Template.Worker.Infrastructure.Autofac
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
