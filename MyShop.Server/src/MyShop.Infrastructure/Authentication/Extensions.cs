using System;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MyShop.Infrastructure.Authentication
{
    public static class Extensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var options = new JwtOptions();
            configuration.Bind("jwt", options);

            services.AddAuthentication()
                .AddJwtBearer(cfg => {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(options.SecretKey)),
                            ValidIssuer = options.Issuer,
                            ValidAudience = options.ValidAudience,
                            ValidateAudience = options.ValidateAudience,
                            ValidateLifetime = options.ValidateLifetime
                    };
                });
        }

        public static void RegisterJwtValidatorMiddleware(this ContainerBuilder builder)
        {
            // builder.Register(ctx => {
            //     var configuration = ctx.Resolve<IConfiguration>();
            //     var options = configuration.GetSection("jwt").Get<JwtOptions>();
            //     return options;
            // }).SingleInstance();
            builder.RegisterType<JwtValidatorMiddleware>().InstancePerDependency();
        }
        
        public static long ToTimestamp(this DateTime dateTime)
        {
            var centuryBegin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedDate = dateTime.Subtract(new TimeSpan(centuryBegin.Ticks));

            return expectedDate.Ticks / 10000;
        }

        public static IApplicationBuilder UseJwtValidatorMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<JwtValidatorMiddleware>();
    }
}