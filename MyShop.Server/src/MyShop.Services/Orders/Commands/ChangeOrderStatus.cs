using System;
using MyShop.Core.Domain.Orders;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands
{
    public class ChangeOrderStatus : ICommand
    {
        public Guid Id { get; }
        public OrderStatus Status { get; }

        [JsonConstructor]
        public ChangeOrderStatus(Guid id,
            OrderStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}