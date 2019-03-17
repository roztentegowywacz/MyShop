using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;

namespace MyShop.Services.Products.Handlers
{
    public class BrowseProductsHandler : IQueryHandler<BrowseProducts, PagedResults<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;

        public BrowseProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<PagedResults<ProductDto>> HandleAsync(BrowseProducts query)
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