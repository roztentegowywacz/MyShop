using System;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands.DeleteProductFromCart
{
    public class DeleteProductFromCartCommand : ICommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public DeleteProductFromCartCommand(Guid customerId, 
            Guid productId, int quantity = 1)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}