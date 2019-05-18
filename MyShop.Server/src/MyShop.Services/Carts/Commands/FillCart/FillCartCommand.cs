using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands.FillCart
{
    public class FillCartCommand : ICommand
    {
        public Guid CustomerId { get; }
        public IEnumerable<CartItem> CartItems { get; } = new List<CartItem>();
        

        [JsonConstructor]
        public FillCartCommand (Guid customerId, IEnumerable<CartItem> cartItems) 
        {
            CustomerId = customerId;
            CartItems = cartItems;
        }

        public class CartItem
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}