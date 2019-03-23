using System.Threading.Tasks;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Commands;

namespace MyShop.Services.Products.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task HandleAsync(CreateProduct command)
        {
            var product = new Product(command.Id, command.Name, 
                command.Description, command.Vendor, 
                command.Price, command.Quantity);

            await _productsRepository.AddAsync(product);
        }
    }
}