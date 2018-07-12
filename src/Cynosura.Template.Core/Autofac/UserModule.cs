using Autofac;
using Cynosura.Template.Core.Services;

namespace Cynosura.Template.Core.Autofac
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}
