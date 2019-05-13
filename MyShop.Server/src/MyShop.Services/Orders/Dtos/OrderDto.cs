using System;
using MyShop.Core.Domain.Orders;

namespace MyShop.Services.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}