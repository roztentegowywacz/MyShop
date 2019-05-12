using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Commands;

namespace MyShop.Services.Products.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICartsRepository _cartsRepository;

        public UpdateProductHandler(IProductsRepository productRepository,
            ICartsRepository cartsRepository)
        {
            _productRepository = productRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(UpdateProduct command)
        {
            var product = await _productRepository.GetAsync(command.Id, true);
            if (product is null)
            {
                throw new NotFoundException("product_not_found",
                    $"Product with an id: '{command.Id}' was not found.");
            }
            if (product.IsDeleted)
            {
                throw new NotFoundException("product_is_deleted",
                    $"Product with an id: '{command.Id}' is deleted.");   
            }
            
            var newProduct = new Product(command.Id, command.Name,
                command.Description, command.Vendor,
                command.Price, command.Quantity);

            await _productRepository.UpdateAsync(newProduct);

            var carts = await _cartsRepository.GetAllWithProducts(command.Id);
            foreach (var cart in carts)
            {
                cart.UpdateProduct(newProduct);                
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}