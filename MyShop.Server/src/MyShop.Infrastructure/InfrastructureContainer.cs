using System.Reflection;
using Autofac;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Products;
using MyShop.Infrastructure.Mongo;

namespace MyShop.Infrastructure
{
    public class InfrastructureContainer
    {
        public static void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.AddMongoDB();
            builder.AddMongoDBRepository<Product>("Products");
            builder.AddMongoDBRepository<User>("Users");
        }
    }
}