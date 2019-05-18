using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands.AddProductToCart
{
    public class AddProductToCartCommand : ICommand 
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public AddProductToCartCommand (Guid customerId, Guid productId, int quantity) 
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}