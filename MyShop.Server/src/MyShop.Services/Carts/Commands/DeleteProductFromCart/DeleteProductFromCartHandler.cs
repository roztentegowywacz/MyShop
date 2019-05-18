using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Carts.Commands.DeleteProductFromCart
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCartCommand>
    {
        private readonly ICartsRepository _cartsRepository;

        public DeleteProductFromCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(DeleteProductFromCartCommand command)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.NullCheck(ErrorCodes.cart_not_found, command.CustomerId);
            
            if (cart.Items.SingleOrDefault(p => p.ProductId == command.ProductId).Quantity > command.Quantity)
            {
                throw new MyShopException(ErrorCodes.invalid_quantity);
            }

            cart.DeleteProduct(command.ProductId, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}