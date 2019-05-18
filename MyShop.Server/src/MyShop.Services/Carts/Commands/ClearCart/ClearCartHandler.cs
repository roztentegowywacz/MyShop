using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Carts.Commands.ClearCart
{
    public class ClearCartHandler : ICommandHandler<ClearCartCommand>
    {
        private readonly ICartsRepository _cartRepository;

        public ClearCartHandler(ICartsRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task HandleAsync(ClearCartCommand command)
        {
            var cart = await _cartRepository.GetAsync(command.CustomerId);
            cart.NullCheck(ErrorCodes.cart_not_found, command.CustomerId);

            cart.Clear();
            await _cartRepository.UpdateAsync(cart);
        }
    }
}