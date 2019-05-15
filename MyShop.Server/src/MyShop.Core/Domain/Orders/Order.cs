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
                throw new MyShopException(ErrorCodes.cannot_create_empty_order);
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
                    throw new MyShopException(ErrorCodes.cannot_approve_approved_order);
                case OrderStatus.Canceled:
                    throw new MyShopException(ErrorCodes.cannot_approve_canceled_order);
                case OrderStatus.Revoked:
                    throw new MyShopException(ErrorCodes.cannot_approve_revoked_order);
                case OrderStatus.Completed:
                    throw new MyShopException(ErrorCodes.cannot_approve_completed_order);
                default:
                    Status = OrderStatus.Approved;
                    break;
            }
        }

        public void Complete()
        {
            if (Status != OrderStatus.Approved)
            {
                throw new MyShopException(ErrorCodes.cannot_complete_not_approved_order);
            }

            switch (Status)
            {
                case OrderStatus.Canceled:
                    throw new MyShopException(ErrorCodes.cannot_complete_canceled_order);
                case OrderStatus.Revoked:
                    throw new MyShopException(ErrorCodes.cannot_complete_revoked_order);
                case OrderStatus.Completed:
                    throw new MyShopException(ErrorCodes.cannot_complete_completed_order);
                default:
                    Status = OrderStatus.Completed;
                    break;
            }
        }

        public void Cancel()
        {
            switch (Status)
            {
                case OrderStatus.Canceled:
                    throw new MyShopException(ErrorCodes.cannot_cancel_canceled_order);
                case OrderStatus.Revoked:
                    throw new MyShopException(ErrorCodes.cannot_cancel_revoked_order);
                case OrderStatus.Completed:
                    throw new MyShopException(ErrorCodes.cannot_cancel_completed_order);
                default:
                    Status = OrderStatus.Canceled;
                    break;
            }
        }

        public void Revoke()
        {
            switch (Status)
            {
                case OrderStatus.Revoked:
                    throw new MyShopException(ErrorCodes.cannot_revoke_revoked_order);
                default:
                    Status = OrderStatus.Revoked;
                    break;
            }
        }
    }
}