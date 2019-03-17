using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyShop.Core.Domain;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public interface IMongoDbRepository<TEntity> where TEntity : IIdentifiable
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResults<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
            TQuery query) where TQuery : IPagedQuery;
    }
}