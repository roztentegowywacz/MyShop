using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartsRepository _cartsRepository;
        private readonly ICustomersRepository _customsRepository;

        public CreateOrderHandler(IOrdersRepository ordersRepository,
            ICartsRepository cartsRepository, ICustomersRepository customsRepository)
        {
            _ordersRepository = ordersRepository;
            _cartsRepository = cartsRepository;
            _customsRepository = customsRepository;
        }

        public async Task HandleAsync(CreateOrderCommand command)
        {
            var customer = await _customsRepository.GetAsync(command.CustomerId);
            customer.NullCheck(ErrorCodes.customer_not_found, command.Id);

            if (!customer.Completed)
            {
                throw new MyShopException("customer_not_completed",
                    $"Can not create an order for not completed custommer with id: '{customer.Id}'.");
            }

            // TODO: zmienić logikę, bo teraz klient może mimeć tylko jedno aktywne zamówineie. Zrobi unikatowe id koszyka.
            if (await _ordersRepository.HasPendingOrderAsync(customer.Id))
            {
                throw new MyShopException("customer_has_pending_order",
                    $"Customer with id: '{customer.Id}' has already a pending order.");
            }

            var cart = await _cartsRepository.GetAsync(customer.Id);
            var order = new Order(command.Id, customer.Id, cart);

            await _ordersRepository.AddAsync(order);

            cart.Clear();
        }
    }
}