using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Orders.Dtos;
using MyShop.Services.Orders.Queries;

namespace MyShop.Services.Orders.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;

        public GetOrderHandler(IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var order = await _ordersRepository.GetAsync(query.Id);
            if (order is null)
            {
                throw new MyShopException("order_not_found",
                    $"Order with id: '{query.Id}' was not found.");
            }

            var customer = await _customersRepository.GetAsync(order.CustomerId);

            return new OrderDto()
            {
                Id = order.Id,
                Status = order.Status.ToString().ToLowerInvariant(),
                Customer = new CustomerDto()
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address
                },
                ItemsCount = order.Cart.Items.Count(),
                TotalAmount = order.TotalAmount,
                Items = order.Cart.Items.Select(i => new OrderItemDto()
                {
                    ProductId = i.ProductId,
                    Name = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                })
            };
        }
    }
}