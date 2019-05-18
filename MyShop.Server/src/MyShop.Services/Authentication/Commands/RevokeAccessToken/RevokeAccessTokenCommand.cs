using System;
using Newtonsoft.Json;

namespace MyShop.Services.Authentication.Commands.RevokeAccessToken
{
    public class RevokeAccessTokenCommand : ICommand
    {
        public string RefreshToken { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public RevokeAccessTokenCommand(string refreshToken, Guid userId)
        {
            RefreshToken = refreshToken;
            UserId = userId;
        }
    }
}