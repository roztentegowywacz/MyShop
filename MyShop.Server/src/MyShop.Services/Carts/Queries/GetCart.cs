using System;
using MyShop.Services.Carts.Dtos;

namespace MyShop.Services.Carts.Queries
{
    public class GetCart : IQuery<CartDto>
    {
        public Guid Id { get; set; }
    }
}