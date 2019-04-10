using MyShop.Core.Domain.Authentication;
using Newtonsoft.Json;

namespace MyShop.Services.Identity.Commands 
{
    public class SignIn : ICommand<JsonWebToken> 
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignIn (string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}