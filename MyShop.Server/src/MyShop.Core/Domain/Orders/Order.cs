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

        public void Approve()
        {
            switch (Status)
            {
                case OrderStatus.Approved:
                    throw new MyShopException("cannot_approve_approved_order",
                        $"Cannot approve an approved order with id: '{Id}'.");
                case OrderStatus.Canceled:
                    throw new MyShopException("cannot_approve_canceled_order",
                        $"Cannot approve a canceled order with id: '{Id}'.");
                case OrderStatus.Revoked:
                    throw new MyShopException("cannot_approve_revoked_order",
                        $"Cannot approve a revoked order with id: '{Id}'.");
                case OrderStatus.Completed:
                    throw new MyShopException("cannot_approve_completed_order",
                        $"Cannot approve a completed order with id: '{Id}'.");
                default:
                    Status = OrderStatus.Approved;
                    break;
            }
        }
    }
}