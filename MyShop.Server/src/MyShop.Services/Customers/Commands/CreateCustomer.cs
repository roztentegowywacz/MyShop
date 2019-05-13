using System;
using Newtonsoft.Json;

namespace MyShop.Services.Customers.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }

        [JsonConstructor]
        public CreateCustomer(Guid id, string email,
            string firstName, string lastName, string address)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;    
        }
    }
}