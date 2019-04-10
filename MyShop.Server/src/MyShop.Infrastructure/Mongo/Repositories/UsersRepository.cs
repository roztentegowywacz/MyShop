using System;
using System.Threading.Tasks;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Identity.Repositories;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoDbRepository<User> _repository;

        public UsersRepository(IMongoDbRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(User user)
            => await _repository.AddAsync(user);
        
        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<User> GetAsync(string email)
            => await _repository.GetAsync(u => u.Email == email.ToLowerInvariant());
    }
}