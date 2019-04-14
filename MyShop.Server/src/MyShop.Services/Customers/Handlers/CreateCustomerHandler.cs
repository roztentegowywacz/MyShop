using System;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Customers;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Services.Customers.Commands;

namespace MyShop.Services.Customers.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ICartsRepository _cartsRepository;

        public CreateCustomerHandler(ICustomersRepository customersRepository,
            ICartsRepository cartsRepository)
        {
            _customersRepository = customersRepository;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            // TODO: zastnowić się jeszcze nad logiką

            var customer = await _customersRepository.GetAsync(command.Id);
            if (customer is null)
            {
                customer = new Customer(command.Id, command.Email);
                await _customersRepository.AddAsync(customer);
            }

            if (customer.Completed)
            {
                throw new MyShopException("customer_already_completed",
                    $"Customer accoutn was already created for user with id: '{command.Id}.'");
            }

            customer.Complete(command.FirstName, command.LastName, command.Address);
            await _customersRepository.UpdateAsync(customer);

            var newCart = new Cart(command.Id);
            await _cartsRepository.AddAsync(newCart);
        }
    }
}