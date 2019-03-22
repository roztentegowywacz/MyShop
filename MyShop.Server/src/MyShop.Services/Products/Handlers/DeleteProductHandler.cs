using System.Threading.Tasks;
using MyShop.Core.Domain;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Commands;

namespace MyShop.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsRepository _productsRepository;

        public DeleteProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task HandleAsync(DeleteProduct command)
        {
            if (!await _productsRepository.ExistsAsync(command.Id))
            {
                throw new NotFoundException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            await _productsRepository.DeleteAsync(command.Id);
        }
    }
}