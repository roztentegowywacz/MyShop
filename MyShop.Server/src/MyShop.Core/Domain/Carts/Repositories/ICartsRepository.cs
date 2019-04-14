using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Carts.Repositories
{
    public interface ICartsRepository
    {
        Task AddAsync(Cart cart);
        Task<Cart> GetAsync(Guid id);
        Task UpdateAsync(Cart cart);
    }
}