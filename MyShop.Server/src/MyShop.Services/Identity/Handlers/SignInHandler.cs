using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Core.Domain.Identity.Repositories;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Identity.Handlers
{
    public class SignInHandler : ICommandHandler<SignIn, JsonWebToken>
    {
        private readonly IUsersRepository _usersRepository;

        public SignInHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<JsonWebToken> HandleAsync(SignIn command)
        {
            throw new System.NotImplementedException();
        }
    }
}