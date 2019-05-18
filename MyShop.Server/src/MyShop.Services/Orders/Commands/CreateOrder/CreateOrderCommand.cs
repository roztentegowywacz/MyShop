using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CreateOrderCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}