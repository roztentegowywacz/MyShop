using System.Threading.Tasks;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries.GetProduct
{
    public class GetProductHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductDto> HandleAsync(GetProductQuery query)
        {
            var product = await _productsRepository.GetAsync(query.Id);
            product.NullCheck(ErrorCodes.product_not_found, query.Id);

            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}