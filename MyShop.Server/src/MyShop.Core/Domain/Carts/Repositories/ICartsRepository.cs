using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Carts.Repositories
{
    public interface ICartsRepository
    {
        Task AddAsync(Cart cart);
        Task<Cart> GetAsync(Guid id);
        Task<IEnumerable<Cart>> GetAllWithProducts(Guid productId);
        Task UpdateAsync(Cart cart);
        Task UpdateManyAsync(IEnumerable<Cart> carts);
    }
}