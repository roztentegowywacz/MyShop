using MyShop.Core.Domain.Identity;

namespace MyShop.Infrastructure.Authentication
{
    public class AdminAuthAttribute : JwtAuthAttribute
    {
        public AdminAuthAttribute() : base(Role.Admin)
        {
        }
    }
}