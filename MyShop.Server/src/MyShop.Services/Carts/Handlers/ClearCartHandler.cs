using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Services.Carts.Commands;

namespace MyShop.Services.Carts.Handlers
{
    public class ClearCartHandler : ICommandHandler<ClearCart>
    {
        private readonly ICartsRepository _cartRepository;

        public ClearCartHandler(ICartsRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task HandleAsync(ClearCart command)
        {
            var cart = await _cartRepository.GetAsync(command.CustomerId);
            if (cart is null)
            {
                throw new NotFoundException("cart_not_found",
                    $"Cart with an id: '{command.CustomerId}' was not found.");
            }

            cart.Clear();
            await _cartRepository.UpdateAsync(cart);
        }
    }
}