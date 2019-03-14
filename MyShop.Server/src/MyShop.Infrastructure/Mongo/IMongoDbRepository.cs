using System;
using System.Threading.Tasks;
using MyShop.Core.Domain;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public interface IMongoDbRepository<TEntity> where TEntity : IIdentifiable
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<PagedResult<TEntity>> BrowseAsync();
    }
}