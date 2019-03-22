using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;

namespace MyShop.Services.Products.Handlers
{
    public class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductDto> HandleAsync(GetProduct query)
        {
            var product = await _productsRepository.GetAsync(query.Id);
            if (product is null)
            {
                throw new NotFoundException("product_not_found",
                    $"Product with an id: '{query.Id}' was not found.");
            }

            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price
            };
        }
    }
}