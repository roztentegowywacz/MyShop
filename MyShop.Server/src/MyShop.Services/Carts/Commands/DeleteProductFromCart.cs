using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands
{
    public class DeleteProductFromCart : ICommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public DeleteProductFromCart(Guid customerId, 
            Guid productId, int quantity = 1)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}