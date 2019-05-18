using System;
using MyShop.Services.Carts.Dtos;

namespace MyShop.Services.Carts.Queries.GetCart
{
    public class GetCartQuery : IQuery<CartDto>
    {
        public Guid Id { get; set; }
    }
}