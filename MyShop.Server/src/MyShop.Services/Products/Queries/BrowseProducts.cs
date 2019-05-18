using System;
using MyShop.Core.Domain.Products;
using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries 
{
    public class BrowseProducts : IPagedFilterQuery<decimal>, IQuery<PagedResults<ProductDto>> 
    {
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 10;
        public string OrderBy { get; set; } = "Name";
        public SortOrder SortOrder { get; set; } = SortOrder.asc;

        public decimal PriceFrom { get; set; } = 0;
        public decimal PriceTo { get; set; } = decimal.MaxValue;


        decimal IFilterQuery<decimal>.ValueFrom {
            get { return this.PriceFrom; }
        }

        decimal IFilterQuery<decimal>.ValueTo {
            get { return this.PriceTo; }
        }
    }
}