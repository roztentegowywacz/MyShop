using MyShop.Core.Domain.Authentication;
using Newtonsoft.Json;

namespace MyShop.Services.Authentication.Commands.RefreshAccessToken
{
    public class RefreshAccessTokenCommand : ICommand<JsonWebToken>
    {
        public string Token { get; }

        [JsonConstructor]
        public RefreshAccessTokenCommand(string token)
        {
            Token = token;
        }
    }
}