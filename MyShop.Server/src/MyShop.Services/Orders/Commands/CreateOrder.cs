using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands
{
    public class CreateOrder : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CreateOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}