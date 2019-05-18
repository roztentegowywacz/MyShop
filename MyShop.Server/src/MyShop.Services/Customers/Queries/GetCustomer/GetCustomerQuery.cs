using System;
using MyShop.Services.Customers.Dtos;

namespace MyShop.Services.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}