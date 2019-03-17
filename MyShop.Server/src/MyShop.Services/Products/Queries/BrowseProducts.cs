using System;
using MyShop.Core.Types;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries 
{
    public class BrowseProducts : IPagedFilterQuery<decimal>, IQuery<PagedResults<ProductDto>> 
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }

        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;


        decimal IFilterQuery<decimal>.ValueFrom {
            get { return this.PriceFrom; }
        }

        decimal IFilterQuery<decimal>.ValueTo {
            get { return this.PriceTo; }
        }
    }
}