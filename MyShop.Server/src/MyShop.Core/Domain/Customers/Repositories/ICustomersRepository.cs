using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Customers.Repositories
{
    public interface ICustomersRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer> GetAsync(Guid id);
        Task UpdateAsync(Customer customer);
    }
}