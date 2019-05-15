using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;
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
            cart.NullCheck(ErrorCodes.cart_not_found);

            cart.Clear();
            await _cartRepository.UpdateAsync(cart);
        }
    }
}