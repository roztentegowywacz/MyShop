using System;
using MyShop.Services.Orders.Dtos;

namespace MyShop.Services.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IQuery<OrderDetailsDto>
    {
        public Guid Id { get; set; }        
    }
}