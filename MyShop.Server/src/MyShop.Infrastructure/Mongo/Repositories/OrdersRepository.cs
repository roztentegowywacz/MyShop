using System;
using System.Threading.Tasks;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoDbRepository<Order> _repository;

        public OrdersRepository(IMongoDbRepository<Order> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Order order)
            => _repository.AddAsync(order);

        public Task<bool> HasPendingOrderAsync(Guid customerId)
            => _repository.ExistsAsync(o => o.CustomerId == customerId &&
                                            (o.Status == OrderStatus.Created ||
                                             o.Status == OrderStatus.Approved));

        public Task<Order> GetAsync(Guid id)
            => _repository.GetAsync(id);
    }
}