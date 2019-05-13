using System.Threading.Tasks;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Customers.Queries;

namespace MyShop.Services.Customers.Handlers
{
    public class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
    {
        private readonly ICustomersRepository _customersRepository;

        public GetCustomerHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<CustomerDto> HandleAsync(GetCustomer query)
        {
            var customer = await _customersRepository.GetAsync(query.Id);
            if (customer is null)
            {
                throw new NotFoundException("customer_not_found",
                    $"Customer with an id: '{query.Id}' was not found.");
            }

            return new CustomerDto()
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address
            };
        }
    }
}