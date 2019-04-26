using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands
{
    public class ClearCart : ICommand
    {

        public Guid CustomerId { get; }   

        [JsonConstructor]     
        public ClearCart(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}