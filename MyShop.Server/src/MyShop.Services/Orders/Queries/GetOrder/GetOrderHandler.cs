using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Orders.Dtos;

namespace MyShop.Services.Orders.Queries.GetOrder
{
    public class GetOrderHandler : IQueryHandler<GetOrderQuery, OrderDetailsDto>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;

        public GetOrderHandler(IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        public async Task<OrderDetailsDto> HandleAsync(GetOrderQuery query)
        {
            var order = await _ordersRepository.GetAsync(query.Id);
            order.NullCheck(ErrorCodes.order_not_found, query.Id);

            var customer = await _customersRepository.GetAsync(order.CustomerId);

            return new OrderDetailsDto()
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