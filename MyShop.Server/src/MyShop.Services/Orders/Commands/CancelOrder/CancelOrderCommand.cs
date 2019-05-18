using System;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CancelOrderCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}