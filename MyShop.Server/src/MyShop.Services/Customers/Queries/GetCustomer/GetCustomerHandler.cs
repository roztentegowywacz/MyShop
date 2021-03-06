using System.Threading.Tasks;
using MyShop.Core.Domain.Customers.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Customers.Dtos;

namespace MyShop.Services.Customers.Queries.GetCustomer
{
    public class GetCustomerHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomersRepository _customersRepository;

        public GetCustomerHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<CustomerDto> HandleAsync(GetCustomerQuery query)
        {
            var customer = await _customersRepository.GetAsync(query.Id);
            customer.NullCheck(ErrorCodes.customer_not_found, query.Id);

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