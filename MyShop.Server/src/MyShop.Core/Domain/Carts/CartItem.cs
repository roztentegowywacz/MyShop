using System;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;

namespace MyShop.Core.Domain.Carts
{
    public class CartItem
    {
        public virtual Product Product { get; set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => Quantity * Product.Price;

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new MyShopException("negative_quantity",
                    "Quantity can not be negative.");
            }

            Quantity += quantity;
        }
    }
}