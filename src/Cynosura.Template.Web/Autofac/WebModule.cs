using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Cynosura.Template.Core.Security;
using Cynosura.Template.Web.Infrastructure;
using Cynosura.Web.Infrastructure;

namespace Cynosura.Template.Web.Autofac
{
    class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValidationExceptionHandler>().As<IExceptionHandler>();
            builder.RegisterType<UserInfoProvider>().As<IUserInfoProvider>().InstancePerLifetimeScope();
        }
    }
}
