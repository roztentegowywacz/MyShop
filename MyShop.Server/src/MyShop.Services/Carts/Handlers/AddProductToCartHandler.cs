using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
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
                throw new MyShopException("invalid_quantity",
                    $"Invalid quantity: '{command.Quantity}'. Quantity can not be negative.");
            }

            var product = await _productsRepository.GetAsync(command.ProductId);
            if (product is null)
            {
                throw new MyShopException("product_not_found",
                    $"Product: '{command.ProductId}' was not found.");
            }

            if (product.Quantity < command.Quantity)
            {
                throw new MyShopException("not_enough_products_in_stock",
                    $"Not enough products in stock: '{command.ProductId}'.");
            }

            // TODO: sprawdzić czy jest cart??
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.AddProduct(product, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}