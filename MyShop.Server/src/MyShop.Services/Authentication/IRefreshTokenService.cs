using System;
using System.Threading.Tasks;

namespace MyShop.Services.Authentication
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);
    }
}