using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries.BrowseProducts
{
    public class BrowseProductsHandler : IQueryHandler<BrowseProductsQuery, PagedResults<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;

        public BrowseProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<PagedResults<ProductDto>> HandleAsync(BrowseProductsQuery query)
        {
            var pagedResult = await _productsRepository.BrowseAsync(query);
            var products = pagedResult.Items.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Vendor = p.Vendor,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();

            return PagedResults<ProductDto>.From(pagedResult, products);
        }
    }
}