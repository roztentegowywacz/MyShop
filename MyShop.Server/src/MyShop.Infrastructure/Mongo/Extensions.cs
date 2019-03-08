using Autofac;
using MongoDB.Driver;
using MyShop.Core.Domain;

namespace MyShop.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this ContainerBuilder builder)
        {
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