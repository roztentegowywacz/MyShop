using System;

namespace MyShop.Services.Orders.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}