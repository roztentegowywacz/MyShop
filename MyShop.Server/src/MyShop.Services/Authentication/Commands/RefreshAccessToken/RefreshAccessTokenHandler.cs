using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Authentication.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity.Repositories;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Authentication.Services;

namespace MyShop.Services.Authentication.Commands.RefreshAccessToken
{
    public class RefreshAccessTokenHandler : ICommandHandler<RefreshAccessTokenCommand, JsonWebToken>
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

        public async Task<JsonWebToken> HandleAsync(RefreshAccessTokenCommand command)
        {
            var refreshToken = await _refreshTokensRepository.GetAsync(command.Token);
            if (refreshToken is null)
            {
                throw new NotFoundException(ErrorCodes.refresh_token_not_found);
            }

            var user = await _usersRepository.GetAsync(refreshToken.UserId);
            user.NullCheck(ErrorCodes.user_not_found, refreshToken.UserId);

            var jwt = _jwtTokenService.CreateToken(user.Id, user.Role);
            jwt.SetRefreshToken(refreshToken.Token);

            return jwt;
        }
    }
}