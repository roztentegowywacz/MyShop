using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Identity.Commands
{
    public class SignUp : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignUp(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}