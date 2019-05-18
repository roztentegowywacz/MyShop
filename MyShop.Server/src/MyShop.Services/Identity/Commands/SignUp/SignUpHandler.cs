using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Customers;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Core.Domain.Identity;
using MyShop.Core.Domain.Identity.Repositories;

namespace MyShop.Services.Identity.Commands.SignUp
{
    public class SignUpHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICartsRepository _cartsRepository;

        public SignUpHandler(IUsersRepository usersRepository,
            IPasswordHasher<User> passwordHasher,
            ICustomersRepository customersRepository,
            ICartsRepository cartsRepository)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _customersRepository = customersRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(SignUpCommand command)
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

            var newCustomer = new Customer(command.Id, command.Email);
            await _customersRepository.AddAsync(newCustomer);

            var newCart = new Cart(command.Id);
            await _cartsRepository.AddAsync(newCart);
        }
    }
}