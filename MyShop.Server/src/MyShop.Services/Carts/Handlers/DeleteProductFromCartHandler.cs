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

            cart.DeleteProduct(command.ProductId);
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}