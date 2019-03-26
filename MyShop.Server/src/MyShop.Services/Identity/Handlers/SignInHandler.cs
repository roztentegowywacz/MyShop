using System.Threading.Tasks;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Identity.Handlers
{
    public class SignInHandler : ICommandHandler<SignIn>
    {
        public Task HandleAsync(SignIn command)
        {
            throw new System.NotImplementedException();
        }
    }
}