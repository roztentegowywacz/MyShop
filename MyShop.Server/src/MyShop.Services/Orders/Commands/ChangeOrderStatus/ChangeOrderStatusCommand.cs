using System;
using MyShop.Core.Domain.Orders;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand : ICommand
    {
        public Guid Id { get; }
        public OrderStatus Status { get; }

        [JsonConstructor]
        public ChangeOrderStatusCommand(Guid id,
            OrderStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}