using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Core.Domain;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MyShop.Infrastructure.Mvc
{
    public static class Extensions
    {
        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
            => services
                .AddMvcCore()
                .AddFluentValidation()
                .AddJsonFormatters()
                .AddDefaultJsonOptions()
                .AddAuthorization(o => o.AddPolicy("admin", p => p.RequireClaim(ClaimTypes.Role, Role.Admin)));

        public static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder)
            => builder.AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        public static void RegisterErrorHandlerMiddleware(this ContainerBuilder builder)
        {
            builder.RegisterType<ErrorHandlerMiddleware>().InstancePerDependency();
        }

        public static void AddValidator<TEntity>(this ContainerBuilder builder, TEntity entityValidator)
        {
            
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static T BindId<T>(this T model, Expression<Func<T, Guid>> expression) where T : IIdentifiable
            => model.Bind<T, Guid>(expression, Guid.NewGuid());

        public static TModel Bind<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression,
            object value)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression is null)
            {
                memberExpression = ((UnaryExpression) expression.Body).Operand as MemberExpression;
            }

            var propertyName = memberExpression.Member.Name.ToLowerInvariant();
            var modelType = model.GetType();
            var field = modelType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .SingleOrDefault(x => x.Name.ToLowerInvariant().StartsWith($"<{propertyName}>"));
            if (field is null)
            {
                return model;
            }

            field.SetValue(model, value);

            return model;
        }

        public static void NullCheck<T>(this T obj, ErrorCodes errorCode, Guid entityId) where T: IIdentifiable
        {
            if (obj == null)
                throw new NotFoundException(errorCode, entityId);
        }

        public static string GetErrorMessage(this Enum value)
            => value.GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<ErrorMessageAttribute>()
                    ?.Message;
                
    }
}