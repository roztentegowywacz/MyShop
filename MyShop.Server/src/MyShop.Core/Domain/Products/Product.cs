using System;
using MyShop.Core.Domain.Exceptions;

namespace MyShop.Core.Domain.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Vendor { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public bool IsDeleted { get; private set; }
        

        public Product(Guid id, string name, string description, string vendor,
            decimal price, int quantity) : base(id)
        {
            SetName(name);
            SetVendor(vendor);
            SetDescription(description);
            SetPrice(price);
            SetQuantity(quantity);
        }
        

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MyShopException("empty_product_name",
                    "Product name cannot be empty.");
            }

            Name = name.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetVendor(string vendor)
        {
            if(string.IsNullOrEmpty(vendor))
            {
                throw new MyShopException("empty_product_vendor", 
                    "Product vendor cannot be empty.");
            }

            Vendor = vendor.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new MyShopException("empty_product_description",
                    "Product description cannot be empty.");
            }

            Description = description.Trim();
            SetUpdatedDate();
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new MyShopException("invalid_product_price",
                    "Product price cannot be zero or negative.");
            }

            Price = price;
            SetUpdatedDate();
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new MyShopException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }

            Quantity = quantity;
            SetUpdatedDate();
        }

        public void Delete()
        {
            if (!IsDeleted)
            {
                throw new MyShopException("product_already_deleted",
                    "Product is aleready deleted.");
            }

            IsDeleted = true;
        }
    }
}