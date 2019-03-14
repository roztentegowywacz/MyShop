using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries
{
    public class BrowseProducts : PagedQueryBase, IQuery<PagedResult<ProductDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}