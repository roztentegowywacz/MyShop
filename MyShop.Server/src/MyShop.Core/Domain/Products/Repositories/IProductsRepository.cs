using System;
using System.Threading.Tasks;
using MyShop.Core.Types;

namespace MyShop.Core.Domain.Products.Repositories
{
    public interface IProductsRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetAsync(Guid id, bool evenIsDeleted = false);
        Task<PagedResults<Product>> BrowseAsync(IPagedFilterQuery<decimal> query);
        Task UpdateAsync(Product product);
    }
}