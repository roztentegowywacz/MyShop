using System;
using System.Threading.Tasks;
using MyShop.Core.Types;

namespace MyShop.Core.Domain.Products.Repositories
{
    public interface IProductsRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetAsync(Guid id);
        Task<PagedResults<Product>> BrowseAsync(IPagedFilterQuery<decimal> query);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}