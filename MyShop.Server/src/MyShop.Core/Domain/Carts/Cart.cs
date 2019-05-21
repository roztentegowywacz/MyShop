using System;
using System.Collections.Generic;
using System.Linq;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Products;

namespace MyShop.Core.Domain.Carts
{
    public class Cart : BaseEntity
    {
        private ISet<CartItem> _items = new HashSet<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get => _items;
            set => _items = new HashSet<CartItem>(value);
        }

        public Cart(Guid userId) : base(userId)
        {
        }

        public void AddProduct(Product product, int quantity)
        {
            var item = GetCartItem(product.Id);
            if (item != null)
            {
                item.IncreaseQuantity(quantity);
                return;
            }
            
            item = new CartItem(product, quantity);
            _items.Add(item);
        }

        public void DeleteProduct(Guid productId, int? quantity = null)
        {
            var item = GetCartItem(productId);
            if (item is null)
            {
                throw new NotFoundException(ErrorCodes.product_not_found);
            }
            if (quantity is null || quantity == item.Quantity)
            {
                _items.Remove(item);
            }
            else
            {
                item.DecreaseQuantity((int)quantity);
                return;
            }

        }

        public void UpdateProduct(Product product)
        {
            var item = GetCartItem(product.Id);
            if (item is null)
            {
                throw new NotFoundException(ErrorCodes.product_not_found);
            }

            item.UpdateProduct(product);
        }

        public void Clear()
            => _items.Clear();

        private CartItem GetCartItem(Guid productId)
            => _items.SingleOrDefault(x => x.ProductId == productId);
    }
}