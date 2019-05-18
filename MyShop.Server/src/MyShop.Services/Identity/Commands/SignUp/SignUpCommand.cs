using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Identity.Commands.SignUp
{
    public class SignUpCommand : ICommand, IIdentifiable
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignUpCommand(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}