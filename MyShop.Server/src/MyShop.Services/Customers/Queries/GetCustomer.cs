using System;
using MyShop.Services.Customers.Dtos;

namespace MyShop.Services.Customers.Queries
{
    public class GetCustomer : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}