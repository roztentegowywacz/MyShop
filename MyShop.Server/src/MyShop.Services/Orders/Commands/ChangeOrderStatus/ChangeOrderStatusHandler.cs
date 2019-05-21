using System.Collections.Generic;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusHandler : ICommandHandler<ChangeOrderStatusCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;

        public ChangeOrderStatusHandler(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            ICartsRepository cartsRepository)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(ChangeOrderStatusCommand command)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            order.NullCheck(ErrorCodes.order_not_found, command.Id);

            switch (command.Status)
            {
                case OrderStatus.Approved:
                {
                    // TODO: ten status w momencie, gdy koszyk zostanie opłacony.
                    order.Approve();
                    await ReserveProductsAsync(order.Cart.Items);
                    break;
                }
                case OrderStatus.Completed:
                {
                    // TODO: ten status w momencie, gdy zamówienie zostanie wysłane
                    order.Complete();
                    var cart = await _cartsRepository.GetAsync(order.Cart.Id);
                    cart.Clear();
                    await _cartsRepository.UpdateAsync(cart);
                    break;
                }
                case OrderStatus.Revoked:
                {
                    order.Revoke();
                    // TODO: to samo dać dla cancel.
                    await ReleaseProductsAsync(order.Cart.Items);
                    break;
                }
                default:
                {
                    throw new MyShopException(ErrorCodes.bad_order_status);
                }
            }

            await _ordersRepository.UpdateAsync(order);
        }

        private async Task ReserveProductsAsync(IEnumerable<CartItem> items)
            => await SetProductsQuantityAsync(items, SetMarker.minus);

        private async Task ReleaseProductsAsync(IEnumerable<CartItem> items)
            => await SetProductsQuantityAsync(items, SetMarker.add);

        private async Task SetProductsQuantityAsync(IEnumerable<CartItem> items, SetMarker setMarker)
        {
            foreach (var item in items)
            {
                var product = await _productsRepository.GetAsync(item.ProductId);
                product.NullCheck(ErrorCodes.product_not_found, item.ProductId);

                var setResultQuantity = product.Quantity + (int)setMarker * item.Quantity;
                if (setResultQuantity < 0)
                {
                    throw new MyShopException("product_out_of_stock",
                        $"Not enough product with id: '{product.Id}', to reserve requested quantity.");
                }

                product.SetQuantity(setResultQuantity);
                await _productsRepository.UpdateAsync(product);
            }
        }

        private enum SetMarker
        {
            add = 1,
            minus = -1
        }
    }
}