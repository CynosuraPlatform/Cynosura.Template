using Autofac;
using Cynosura.Template.Core.Services;

namespace Cynosura.Template.Core.Autofac
{
    public class RoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleService>().As<IRoleService>();
        }
    }
}
