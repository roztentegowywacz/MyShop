using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Orders.Repositories
{
    public interface IOrdersRepository
    {
        Task AddAsync(Order order);
        Task<bool> HasPendingOrderAsync(Guid customerId);
        Task<Order> GetAsync(Guid id);
    }
}