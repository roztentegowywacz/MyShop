using System.Threading.Tasks;
using MyShop.Core.Domain;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Commands;

namespace MyShop.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;

        public DeleteProductHandler(IProductsRepository productsRepository,
            ICartsRepository cartsRepository)
        {
            _productsRepository = productsRepository;
            _cartsRepository = cartsRepository;
        }
        
        public async Task HandleAsync(DeleteProduct command)
        {
            var product = await _productsRepository.GetAsync(command.Id, true);
            if (product is null)
            {
                throw new NotFoundException("product_not_found",
                    $"Product with an id: '{command.Id}' was not found.");
            }

            product.Delete();
            
            await _productsRepository.UpdateAsync(product);

            var carts = await _cartsRepository.GetAllWithProducts(command.Id);
            foreach (var cart in carts)
            {
                cart.DeleteProduct(command.Id);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}