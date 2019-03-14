using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyShop.Core.Domain;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : IIdentifiable
    {
        protected IMongoCollection<TEntity> Collection { get; }

        public MongoDbRepository(IMongoDatabase database, string collectionName)
		{
			Collection = database.GetCollection<TEntity>(collectionName);
		}

        public async Task AddAsync(TEntity entity)
            => await Collection.InsertOneAsync(entity);

        public async Task<TEntity> GetAsync(Guid id)
            => await GetAsync(e => e.Id == id);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await Collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query) where TQuery : PagedQueryBase
        {
            var a = Collection.AsQueryable().Where(predicate);
        }
    }
}