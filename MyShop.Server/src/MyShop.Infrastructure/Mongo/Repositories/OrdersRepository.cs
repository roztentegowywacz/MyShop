using System;
using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoDbRepository<Order> _repository;

        public OrdersRepository(IMongoDbRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Order order)
            => await _repository.AddAsync(order);

        public async Task<bool> HasPendingOrderAsync(Guid customerId)
            => await _repository.ExistsAsync(o => o.CustomerId == customerId &&
                                            (o.Status == OrderStatus.Created ||
                                             o.Status == OrderStatus.Approved));

        public async Task<Order> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<PagedResults<Order>> BrowseAsync(IPagedFilterQuery<OrderStatus> query)
            => await _repository.BrowseAsync(
                                    o => o.Status == query.ValueFrom, query);

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order);
    }
}