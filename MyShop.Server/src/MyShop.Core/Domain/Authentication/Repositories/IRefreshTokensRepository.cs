using System;
using System.Threading.Tasks;

namespace MyShop.Core.Domain.Authentication.Repositories
{
    public interface IRefreshTokensRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetAsync(string token);
        Task DeleteAsync(Guid id);
    }
}