using System;
using MyShop.Services.Products.Dtos;

namespace MyShop.Services.Products.Queries
{
    public class GetProduct : IQuery<ProductDto>
    {
        public Guid Id { get; set; }
    }
}