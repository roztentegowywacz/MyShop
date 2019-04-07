using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Authentication.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity.Repositories;
using MyShop.Services.Authentication.Commands;
using MyShop.Services.Authentication.Services;

namespace MyShop.Services.Authentication.Handlers
{
    public class RefreshAccessTokenHandler : ICommandHandler<RefreshAccessToken, JsonWebToken>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshAccessTokenHandler(
            IRefreshTokensRepository refreshTokensRepository, 
            IUsersRepository usersRepository,
            IJwtTokenService jwtTokenService)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _usersRepository = usersRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<JsonWebToken> HandleAsync(RefreshAccessToken command)
        {
            var refreshToken = await _refreshTokensRepository.GetAsync(command.Token);
            if (refreshToken is null)
            {
                throw new MyShopException("refresh_token_not_found",
                    "Refresh token was not found");
            }

            var user = await _usersRepository.GetAsync(refreshToken.UserId);
            if (user is null)
            {
                throw new MyShopException("user_not_found",
                    $"User: '{refreshToken.UserId}' was not found.");
            }

            var jwt = _jwtTokenService.CreateToken(user.Id, user.Role);
            jwt.SetRefreshToken(refreshToken.Token);

            return jwt;
        }
    }
}