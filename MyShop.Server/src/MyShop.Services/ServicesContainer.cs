using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Identity;
using MyShop.Infrastructure.FluentValidation;
using MyShop.Services.Products.Commands.CreateProduct;

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

            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();

            builder.AddValidator<CreateProductValidator>();
        }
    }
}