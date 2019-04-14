using System;
using System.Collections.Generic;

namespace MyShop.Services.Carts.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; }
    }
}