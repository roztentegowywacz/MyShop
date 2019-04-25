using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Carts.Repositories;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly IMongoDbRepository<Cart> _repository;

        public CartsRepository(IMongoDbRepository<Cart> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Cart cart)
            => await _repository.AddAsync(cart);

        public async Task<Cart> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<IEnumerable<Cart>> GetAllWithProducts(Guid productId)
            => await _repository.FindAsync(c => c.Items.Any(i => i.Product.Id == productId));

        public async Task UpdateAsync(Cart cart)
            => await _repository.UpdateAsync(cart);

        public async Task UpdateManyAsync(IEnumerable<Cart> carts)
        {
            foreach (var cart in carts)
            {
                await _repository.UpdateAsync(cart);
            }
        }
    }
}