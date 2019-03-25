using System.Threading.Tasks;

namespace MyShop.Core.Domain.Identity.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(string email);
    }
}