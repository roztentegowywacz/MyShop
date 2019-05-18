using System.Reflection;
using Autofac;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Customers;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Products;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mongo;
using MyShop.Infrastructure.Mvc;

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
            builder.RegisterJwtValidatorMiddleware();
            builder.RegisterErrorHandlerMiddleware();
            builder.AddMongoDBRepository<Product>("Products");
            builder.AddMongoDBRepository<User>("Users");
            builder.AddMongoDBRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoDBRepository<Cart>("Carts");
            builder.AddMongoDBRepository<Customer>("Customers");
            builder.AddMongoDBRepository<Order>("Orders");
            // builder.AddValidator<Product, ProductValidator>();
        }
    }
}