using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public static class Pagination
    {
        // TODO: null check!!

        public static async Task<PagedResults<TEntity>> PaginateAsync<TEntity, TQuery>(this IMongoQueryable<TEntity> collection,
            TQuery query) where TQuery : IPagedQuery
        {
            var isEmpty = await collection.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResults<TEntity>.Empty;
            }


            var totalResults = await collection.CountAsync();
            var totalPages = (int)(totalResults / query.ResultsPerPage) + 1;           
            var data = await collection.Limit(query).ToListAsync();

            return PagedResults<TEntity>.Create(data, query.Page, query.ResultsPerPage, totalPages, totalResults);
        }

        private static IMongoQueryable<TEntity> Limit<TEntity, TQuery>(this IMongoQueryable<TEntity> collection, 
            TQuery query) where TQuery : IPagedQuery
        {
            var skip = (query.Page - 1) * query.ResultsPerPage;
            var data = collection.Skip(skip).Take(query.ResultsPerPage);

            return data;
        }        
    }
}