using System;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;

namespace MyShop.Core.Domain.Carts
{
    public class CartItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public CartItem(Product product, int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            UnitPrice = product.Price;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new MyShopException(ErrorCodes.negative_quantity);
            }

            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new MyShopException(ErrorCodes.negative_quantity);
            }
            if (quantity > Quantity)
            {
                throw new MyShopException(ErrorCodes.invalid_quantity);
            }

            Quantity -= quantity;
        }

        public void UpdateProduct(Product product)
        {
            ProductName = product.Name;
            UnitPrice = product.Price;
        }
    }
}