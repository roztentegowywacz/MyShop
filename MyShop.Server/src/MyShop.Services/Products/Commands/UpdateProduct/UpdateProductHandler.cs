using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICartsRepository _cartsRepository;

        public UpdateProductHandler(IProductsRepository productRepository,
            ICartsRepository cartsRepository)
        {
            _productRepository = productRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(UpdateProductCommand command)
        {
            var product = await _productRepository.GetAsync(command.Id, true);
            product.NullCheck(ErrorCodes.product_not_found, command.Id);
            
            if (product.IsDeleted)
            {
                throw new NotFoundException(ErrorCodes.product_is_deleted);   
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