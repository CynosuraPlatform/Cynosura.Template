using System;
using System.Linq;
using Autofac;
using MediatR;

namespace Cynosura.Template.Core.Autofac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            RegisterAllRequestHandlers(builder);
        }

        private void RegisterAllRequestHandlers(ContainerBuilder builder)
        {
            var handlerTypes = new [] { typeof(IRequestHandler<>), typeof(IRequestHandler<,>) };
            var handlers = typeof(CoreModule).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => handlerTypes.Any(ht => ht.Name == i.Name)))
                .ToList();

            foreach (var handler in handlers)
            {
                builder.RegisterType(handler).AsImplementedInterfaces();
            }
        }
    }
}
