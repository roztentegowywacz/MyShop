using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands.ClearCart
{
    public class ClearCartCommand : ICommand
    {

        public Guid CustomerId { get; }   

        [JsonConstructor]     
        public ClearCartCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}