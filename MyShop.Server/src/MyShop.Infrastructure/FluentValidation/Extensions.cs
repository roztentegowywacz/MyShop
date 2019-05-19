using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyShop.Infrastructure.FluentValidation
{
    public static class Extensions
    {
        public static IMvcCoreBuilder AddCustomFluentValidation(this IMvcCoreBuilder builder)
            => builder
                .AddFluentValidation(fv => 
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

        public static void AddValidator<TValidator>(this ContainerBuilder builder) 
            where TValidator : IValidator
            => builder.RegisterType<TValidator>()
                .As(typeof(TValidator).BaseType)
                .InstancePerLifetimeScope();
    }
}