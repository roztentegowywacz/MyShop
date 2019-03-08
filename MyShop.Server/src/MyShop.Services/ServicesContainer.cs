using System.Reflection;
using Autofac;

namespace MyShop.Services
{
    public class ServicesContainer
    {
        public static void Load(ContainerBuilder builder)
        {
            var servicesAssebly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(servicesAssebly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}