using System;
using System.Linq.Expressions;
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

        public async Task<Product> GetAsync(Guid id, bool evenIsDeleted = false)
        {
            Expression<Func<Product, bool>> predicate; 
            if (evenIsDeleted)
            {
                predicate = p => p.Id == id && !p.IsDeleted;
            }
            else 
            {
                predicate = p => p.Id == id; 
            }
            return await _repository.GetAsync(predicate);
        }

        public async Task<PagedResults<Product>> BrowseAsync(IPagedFilterQuery<decimal> query)
            => await _repository.BrowseAsync(
                                    p => p.Price >= query.ValueFrom && p.Price <= query.ValueTo, 
                                    query);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);
    }
}