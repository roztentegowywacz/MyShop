using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Authentication.Repositories;

namespace MyShop.Infrastructure.Mongo.Repositories
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly IMongoDbRepository<RefreshToken> _repository;

        public RefreshTokensRepository(IMongoDbRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(RefreshToken refreshToken)
            => await _repository.AddAsync(refreshToken);

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);
    }
}