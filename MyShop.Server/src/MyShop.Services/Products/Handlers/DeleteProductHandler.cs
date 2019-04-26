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
            if (!await _productsRepository.ExistsAsync(command.Id))
            {
                throw new NotFoundException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            await _productsRepository.DeleteAsync(command.Id);

            var carts = await _cartsRepository.GetAllWithProducts(command.Id);
            foreach (var cart in carts)
            {
                cart.DeleteProduct(command.Id);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}