using System;
using System.Threading.Tasks;
using MyShop.Core.Domain.Customers;
using MyShop.Core.Domain.Customers.Repositories;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMongoDbRepository<Customer> _repository;

        public CustomersRepository(IMongoDbRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task UpdateAsync(Customer customer)
            => await _repository.UpdateAsync(customer);        
    }
}