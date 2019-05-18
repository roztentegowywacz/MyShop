using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartsRepository;

        public DeleteProductHandler(IProductsRepository productsRepository,
            ICartsRepository cartsRepository)
        {
            _productsRepository = productsRepository;
            _cartsRepository = cartsRepository;
        }
        
        public async Task HandleAsync(DeleteProductCommand command)
        {
            var product = await _productsRepository.GetAsync(command.Id, true);
            product.NullCheck(ErrorCodes.product_not_found, command.Id);

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