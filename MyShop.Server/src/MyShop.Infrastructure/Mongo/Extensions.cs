using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MyShop.Core.Domain;

namespace MyShop.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IConfiguration>();
                var options = configuration.GetValue<MongoDbOptions>("mongo");
                return options;
            }).SingleInstance();

            builder.Register(ctx =>
            {
                var options = ctx.Resolve<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            builder.Register(ctx =>
            {
                var options = ctx.Resolve<MongoDbOptions>();
                var client = ctx.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);
            }).InstancePerLifetimeScope();

            builder.RegisterType<MongoDbInitializer>()
                .As<IMongoDbInitializer>()
                .InstancePerLifetimeScope();
        }

        public static void AddMongoDBRepository<TEntity>(this ContainerBuilder builder, string collectionName)
            where TEntity : IIdentifiable
            => builder.Register(ctx => new MongoRepository<TEntity>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IMongoRepository<TEntity>>()
                .InstancePerLifetimeScope();
    }
}