using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyShop.Services.Carts.Commands
{
    public class FillCart : ICommand
    {
        public Guid CustomerId { get; }
        public IEnumerable<CartItem> CartItems { get; } = new List<CartItem>();
        

        [JsonConstructor]
        public FillCart (Guid customerId, IEnumerable<CartItem> cartItems) 
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