using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Products.Repositories
{
    public interface IProductsRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetAsync(Guid id);
    }
}