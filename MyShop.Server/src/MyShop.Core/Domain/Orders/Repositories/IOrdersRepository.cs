using System;
using System.Threading.Tasks;
using MyShop.Core.Types;

namespace MyShop.Core.Domain.Orders.Repositories
{
    public interface IOrdersRepository
    {
        Task AddAsync(Order order);
        Task<bool> HasPendingOrderAsync(Guid customerId);
        Task<Order> GetAsync(Guid id);
        Task<PagedResults<Order>> BrowseAsync(IPagedFilterQuery<OrderStatus> query);
        Task UpdateAsync(Order order);
    }
}