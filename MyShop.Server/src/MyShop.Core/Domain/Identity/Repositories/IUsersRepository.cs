using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Identity.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
    }
}