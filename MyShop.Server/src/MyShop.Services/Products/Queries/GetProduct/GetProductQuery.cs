using System;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries.GetProduct
{
    public class GetProductQuery : IQuery<ProductDto>
    {
        public Guid Id { get; set; }
    }
}