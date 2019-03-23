using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo
{
    public static class Pagination
    {
        // TODO: Add Order By and Sort Order
        public static async Task<PagedResults<TEntity>> PaginateAsync<TEntity, TQuery>(this IMongoQueryable<TEntity> collection,
            TQuery query) where TQuery : IPagedQuery 
            => await collection.PaginateAsync<TEntity, TQuery>(query.Page, query.ResultsPerPage);

        private static async Task<PagedResults<TEntity>> PaginateAsync<TEntity, TQuery>(this IMongoQueryable<TEntity> collection,
            int page, int resultsPerPage)
        {
            page = page <= 0 ? 1 : page;
            resultsPerPage = resultsPerPage <= 0 ? 10 : resultsPerPage; 

            var isEmpty = await collection.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResults<TEntity>.Empty;
            }


            var totalResults = await collection.CountAsync();
            var totalPages = (int)(totalResults / resultsPerPage) + 1;  

            var skip = (page - 1) * resultsPerPage;
            var data = await collection.Skip(skip).Take(resultsPerPage).ToListAsync();

            return PagedResults<TEntity>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }     
    }
}