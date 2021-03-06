using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;

namespace MyShop.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartsRepository _cartsRepository;

        public CreateOrderHandler(IOrdersRepository ordersRepository,
            ICartsRepository cartsRepository)
        {
            _ordersRepository = ordersRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(CreateOrderCommand command)
        {
            // TODO: zmienić logikę, bo teraz klient może mimeć tylko jedno aktywne zamówineie. Zrobi unikatowe id koszyka.
            // TODO: żeby złożyć zamównienie to klient musi uzupełnić swoje dane.
            if (await _ordersRepository.HasPendingOrderAsync(command.CustomerId))
            {
                throw new MyShopException("customer_has_pending_order",
                    $"Customer with id: '{command.CustomerId}' has already a pending order.");
            }

            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            var order = new Order(command.Id, command.CustomerId, cart);

            await _ordersRepository.AddAsync(order);

            cart.Clear();
        }
    }
}