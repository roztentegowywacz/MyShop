using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Services.Carts.Commands;

namespace MyShop.Services.Carts.Handlers
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCart>
    {
        private readonly ICartsRepository _cartsRepository;

        public DeleteProductFromCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(DeleteProductFromCart command)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            if (cart is null)
            {
                throw new MyShopException("cart_not_found",
                    $"Cart: '{command.CustomerId}' was not found.");
            }
            if (cart.Items.SingleOrDefault(p => p.ProductId == command.ProductId).Quantity > command.Quantity)
            {
                throw new MyShopException("invalid_quantity",
                    $"Quantity for product: '{command.ProductId}' " +
                    $"is lower than given quantity: '{command.Quantity}'.");
            }

            cart.DeleteProduct(command.ProductId, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}