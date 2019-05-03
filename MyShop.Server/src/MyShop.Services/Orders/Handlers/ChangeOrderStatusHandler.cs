using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Services.Orders.Commands;

namespace MyShop.Services.Orders.Handlers
{
    public class ChangeOrderStatusHandler : ICommandHandler<ChangeOrderStatus>
    {
        private readonly IOrdersRepository _ordersRepository;

        public ChangeOrderStatusHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(ChangeOrderStatus command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order is null)
            {
                throw new MyShopException("order_not_found",
                    $"Order with id: '{command.Id}' was not found.");
            }

            switch (command.Status)
            {
                case OrderStatus.Approved:
                    order.Approve();
                    break;
                case OrderStatus.Completed:
                    order.Complete();
                    break;
                case OrderStatus.Revoked:
                    order.Revoke();
                    break;
                default:
                    throw new MyShopException("bad_order_status",
                        $"Given order status: '{command.Status}' not exists.");
            }

            await _ordersRepository.UpdateAsync(order);
        }
    }
}