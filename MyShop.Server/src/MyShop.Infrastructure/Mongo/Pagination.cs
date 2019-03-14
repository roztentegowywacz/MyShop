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

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IMongoQueryable<Task> collection,
            PagedQueryBase query)
        {
            var isEmpty = await collection.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResult<T>.Empty;
            }

            var totalResults = await collection.CountAsync();
            var totalPages = (int)(totalResults / query.ResultsPerPage) + 1;
            var data = await collection.Limit(query).ToListAsync();
        }

        private static IMongoQueryable<T> Limit<T>(this IMongoQueryable<T> collection, PagedQueryBase query)
        {
            var skip = (query.Page - 1) * query.ResultsPerPage;
            var data = collection.Skip(skip).Take(query.ResultsPerPage);

            return data;
        }        
    }
}