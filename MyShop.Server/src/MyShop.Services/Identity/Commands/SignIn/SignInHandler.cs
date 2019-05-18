using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Authentication.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Identity.Repositories;
using MyShop.Services.Authentication.Services;

namespace MyShop.Services.Identity.Commands.SignIn
{
    public class SignInHandler : ICommandHandler<SignInCommand, JsonWebToken>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public SignInHandler(IUsersRepository usersRepository,
            IPasswordHasher<User> passwordHasher,
            IRefreshTokensRepository refreshTokensRepository,
            IJwtTokenService jwtTokenService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _refreshTokensRepository = refreshTokensRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<JsonWebToken> HandleAsync(SignInCommand command)
        {
            var user = await _usersRepository.GetAsync(command.Email);
            if (user is null)
            {
                throw new MyShopException("invalid_credentials",
                    "Invalid credenctials.");
            }

            var refreshToken = new RefreshToken(user, _passwordHasher);
            var jwt = _jwtTokenService.CreateToken(user.Id, user.Role);
            jwt.SetRefreshToken(refreshToken.Token);

            await _refreshTokensRepository.AddAsync(refreshToken);

            return jwt;
        }
    }
}