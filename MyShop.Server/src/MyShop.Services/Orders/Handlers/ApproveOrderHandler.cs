using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Services.Orders.Commands;

namespace MyShop.Services.Orders.Handlers
{
    public class ApproveOrderHandler : ICommandHandler<ApproveOrder>
    {
        private readonly IOrdersRepository _ordersRepository;

        public ApproveOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(ApproveOrder command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order is null)
            {
                throw new MyShopException("order_not_found",
                    $"order with id: '{command.Id}' was not found.");
            }

            order.Approve();

            await _ordersRepository.UpdateAsync(order);
        }
    }
}