using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders.Repositories;

namespace MyShop.Services.Orders.Commands.CancelOrder
{
    public class CancelOrderHandler : ICommandHandler<CancelOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;

        public CancelOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(CancelOrderCommand command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order is null || order.CustomerId != command.CustomerId)
            {
                throw new MyShopException("order_not_found", 
                    $"Order with id: '{command.Id}' " +
                    $"was not found for customer with id: '{command.CustomerId}'.");
            }

            order.Cancel();
            
            await _ordersRepository.UpdateAsync(order);
        }
    }
}