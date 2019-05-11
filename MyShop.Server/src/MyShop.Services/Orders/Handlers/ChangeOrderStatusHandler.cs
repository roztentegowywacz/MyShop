using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Orders;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Orders.Commands;

namespace MyShop.Services.Orders.Handlers
{
    public class ChangeOrderStatusHandler : ICommandHandler<ChangeOrderStatus>
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
                    // TODO: ten status w momencie, gdy koszyk zostanie opłacony.
                    order.Approve();
                    await ReserveProductsAsync(order.Cart.Items);
                    break;
                case OrderStatus.Completed:
                    // TODO: ten status w momencie, gdy zamówienie zostanie wysłane
                    order.Complete();
                    var cart = await _cartsRepository.GetAsync(order.Cart.Id);
                    cart.Clear();
                    await _cartsRepository.UpdateAsync(cart);
                    break;
                case OrderStatus.Revoked:
                    order.Revoke();
                    // TODO: to samo dać dla cancel.
                    await ReleaseProductsAsync(order.Cart.Items);
                    break;
                default:
                    throw new MyShopException("bad_order_status",
                        $"Given order status: '{command.Status}' not exists.");
            }

            await _ordersRepository.UpdateAsync(order);
        }

        // TODO: obsłużyć przypadek gdy nie ma wystarczającej ilości produktów w magazynie.
        private async Task ReserveProductsAsync(IEnumerable<CartItem> items)
            => await SetProductsQuantityAsync(items, SetMarker.minus);

        private async Task ReleaseProductsAsync(IEnumerable<CartItem> items)
            => await SetProductsQuantityAsync(items, SetMarker.add);

        private async Task SetProductsQuantityAsync(IEnumerable<CartItem> items, SetMarker setMarker)
        {
            foreach (var item in items)
            {
                var product = await _productsRepository.GetAsync(item.ProductId);
                if (product is null)
                {
                    throw new MyShopException("product_not_found",
                        $"Product with id: '{product.Id}' was not found.");
                }

                product.SetQuantity(product.Quantity + (int)setMarker * item.Quantity);
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