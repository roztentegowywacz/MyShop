using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Authentication.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Identity.Repositories;

namespace MyShop.Services.Authentication
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RefreshTokenService(IUsersRepository usersRepository,
            IRefreshTokensRepository refreshTokensRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _usersRepository = usersRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task AddAsync(Guid userId)
        {
            var user = await _usersRepository.GetAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("user_not_found",
                    $"User: '{userId}' was not found.");
            }
            await _refreshTokensRepository.AddAsync(new RefreshToken(user, _passwordHasher));
        }
    }
}