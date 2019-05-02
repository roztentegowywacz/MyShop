using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands
{
    public class CreateOrder : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public Guid CustomerId { get; } // TODO: nie po id klienta tylko po id koszyka, bo teraz możemy zrobić tylko jedno zamówienie na klienta

        [JsonConstructor]
        public CreateOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}