using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MyShop.Core.Domain.Authentication;
using MyShop.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;

namespace MyShop.Services.Authentication.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private static readonly ISet<string> DefaultClaims = new HashSet<string>()
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
        };

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenService(JwtOptions options, IHttpContextAccessor httpContextAccessor)
        {
            _options = options;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public JsonWebToken CreateToken(Guid userId, string role)
        {
            var userIdString = userId.ToString("N");
            if (string.IsNullOrWhiteSpace(userIdString))
            {
                throw new ArgumentException("User id claim can not be empty.", nameof(userIdString));
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userIdString),
                new Claim(JwtRegisteredClaimNames.UniqueName, userIdString),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken(userId, token, string.Empty, expires.ToTimestamp());
        }
    }
}