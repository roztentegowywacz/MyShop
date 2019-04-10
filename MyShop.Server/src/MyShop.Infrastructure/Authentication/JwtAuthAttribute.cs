using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MyShop.Infrastructure.Authentication
{
    public class JwtAuthAttribute : AuthorizeAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(policy)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}