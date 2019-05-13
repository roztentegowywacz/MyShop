using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands 
{
    public class AddProductToCart : ICommand 
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public AddProductToCart (Guid customerId, Guid productId, int quantity) 
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}