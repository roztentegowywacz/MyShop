using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Identity.Repositories;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Identity.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public SignUpHandler(IUsersRepository usersRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task HandleAsync(SignUp command)
        {
            var user = await _usersRepository.GetAsync(command.Email);
            if (user != null)
            {
                throw new MyShopException("email_in_use",
                    $"Email: '{command.Email}' as already in use.");
            }

            user = new User(command.Id, command.Email, Role.User);
            user.SetPassword(command.Password, _passwordHasher);

            await _usersRepository.AddAsync(user);
        }
    }
}