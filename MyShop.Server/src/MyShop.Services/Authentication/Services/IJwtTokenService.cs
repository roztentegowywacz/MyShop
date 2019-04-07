using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;

namespace MyShop.Services.Authentication.Services
{
    public interface IJwtTokenService
    {
        JsonWebToken CreateToken(Guid userId, string role);
    }
}