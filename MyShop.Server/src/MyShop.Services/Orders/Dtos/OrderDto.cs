using System;
using System.Collections.Generic;
using MyShop.Services.Customers.Dtos;

namespace MyShop.Services.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public CustomerDto Customer { get; set; } 
        public int ItemsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }  
    }
}