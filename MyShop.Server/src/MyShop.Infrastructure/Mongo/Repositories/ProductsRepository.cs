using System;
using System.Threading.Tasks;
using MyShop.Core.Domain.Products;
using MyShop.Core.Domain.Products.Repositories;
using MyShop.Core.Types;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoDbRepository<Product> _repository;

        public ProductsRepository(IMongoDbRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Product product)
            => await _repository.AddAsync(product);

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<PagedResult<Product>> BrowseAsync(BrowseProducts query)
    }
}