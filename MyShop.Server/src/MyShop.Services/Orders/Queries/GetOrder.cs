using System;
using MyShop.Services.Orders.Dtos;

namespace MyShop.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid Id { get; set; }        
    }
}