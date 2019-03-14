using System.Threading.Tasks;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;

namespace MyShop.Services.Products.Handlers
{
    public class BrowseProductsHandler : IQueryHandler<BrowseProducts, PagedResult<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;

        public BrowseProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Task<PagedResult<ProductDto>> HandleAsync(BrowseProducts query)
        {
            var pagedResult = await _productsRepository
        }
    }
}