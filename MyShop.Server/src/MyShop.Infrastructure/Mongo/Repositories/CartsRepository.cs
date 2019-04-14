using System;
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

        public async Task UpdateAsync(Cart cart)
            => await _repository.UpdateAsync(cart);
    }
}