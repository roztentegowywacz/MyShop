using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

        public async Task<PagedResults<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query) where TQuery : IPagedQuery
            => await Collection.AsQueryable().Where(predicate).PaginateAsync(query); 

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await Collection.Find(predicate).AnyAsync();

        public async Task DeleteAsync(Guid id)
            => await Collection.DeleteOneAsync(e => e.Id == id);
    }
         
}