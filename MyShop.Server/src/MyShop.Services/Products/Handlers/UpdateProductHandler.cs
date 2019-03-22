using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Commands;

namespace MyShop.Services.Products.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsRepository _productRepository;

        public UpdateProductHandler(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(UpdateProduct command)
        {
            var product = await _productRepository.GetAsync(command.Id);
            if (product is null)
            {
                throw new NotFoundException("product_not_found",
                    $"Product with an id: '{command.Id}' was not found.");
            }

            product.SetDescription(command.Description);
            product.SetName(command.Name);
            product.SetPrice(command.Price);
            product.SetQuantity(command.Quantity);
            product.SetVendor(command.Vendor);

            await _productRepository.UpdateAsync(product);
        }
    }
}