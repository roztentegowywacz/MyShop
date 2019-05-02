using System;
using System.Linq;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Exceptions;

namespace MyShop.Core.Domain.Orders
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public Cart Cart { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid id, Guid customerId, Cart cart) : base(id)
        {
            if (cart is null || !cart.Items.Any())
            {
                throw new MyShopException("cennot_create_empty_order",
                    $"Cannot create an order with invalid currency for customer with id: '{customerId}'.");
            }

            CustomerId = customerId;
            Cart = cart;
            Status = OrderStatus.Created;
            TotalAmount = Cart.Items.Sum(i => i.TotalPrice);
        }
    }
}