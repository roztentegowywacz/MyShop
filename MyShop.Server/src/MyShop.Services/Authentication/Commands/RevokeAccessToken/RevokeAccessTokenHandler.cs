using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MyShop.Core.Domain.Authentication.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Authentication;

namespace MyShop.Services.Authentication.Commands.RevokeAccessToken
{
    public class RevokeAccessTokenHandler : ICommandHandler<RevokeAccessTokenCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public RevokeAccessTokenHandler(
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache cache,
            IOptions<JwtOptions> jwtOptions,
            IRefreshTokensRepository refreshTokensRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _jwtOptions = jwtOptions;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task HandleAsync(RevokeAccessTokenCommand command)
        {
            var refreshToken = await _refreshTokensRepository.GetAsync(command.RefreshToken);
            if (refreshToken is null || refreshToken.UserId != command.UserId)
            {
                throw new MyShopException("refresh_token_not_found",
                    $"Refresh token: '{command.RefreshToken}' was not hound.");
            }

            await _refreshTokensRepository.DeleteAsync(refreshToken.Id);
            
            await _cache.SetStringAsync($"tokens:{GetCurrentAsync()}",
                "revoked", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan
                        .FromMinutes(_jwtOptions.Value.ExpiryMinutes)
                });
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return string.IsNullOrWhiteSpace(authorizationHeader)
                ? string.Empty
                : authorizationHeader.Single().Split(' ').Last();
        }
    }
}