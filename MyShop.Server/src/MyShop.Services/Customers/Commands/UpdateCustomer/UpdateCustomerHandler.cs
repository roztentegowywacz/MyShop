using System.Threading.Tasks;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Services.Customers.Commands.CompleteCustomer
{
    public class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly ICustomersRepository _customersRepository;

        public UpdateCustomerHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(UpdateCustomerCommand command)
        {
            var customer = await _customersRepository.GetAsync(command.Id);
            customer.NullCheck(ErrorCodes.customer_not_found, command.Id);

            if (!customer.Completed)
            {
                throw new MyShopException("customer_not_completed",
                    $"Customer accoutn was not created yet for user with id: '{command.Id}.'");
            }

            customer.Update(command.Email, command.FirstName, command.LastName, command.Address);
            await _customersRepository.UpdateAsync(customer);
        }
    }
}