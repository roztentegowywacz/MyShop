using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Carts.Commands;

namespace MyShop.Services.Carts.Handlers
{
    public class AddProductToCartHandler : ICommandHandler<AddProductToCart>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;

        public AddProductToCartHandler(IProductsRepository productsRepository,
            ICartsRepository cartsRepository)
        {
            _productsRepository = productsRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(AddProductToCart command)
        {
            if (command.Quantity <= 0)
            {
                throw new MyShopException(ErrorCodes.negative_quantity);
            }

            var product = await _productsRepository.GetAsync(command.ProductId);
            product.NullCheck(ErrorCodes.product_not_found, command.ProductId);

            if (product.Quantity < command.Quantity)
            {
                throw new MyShopException(ErrorCodes.not_enough_products_in_stock);
            }

            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.NullCheck(ErrorCodes.cart_not_found, command.CustomerId);

            cart.AddProduct(product, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}