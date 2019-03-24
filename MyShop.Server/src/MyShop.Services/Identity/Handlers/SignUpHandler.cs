using System.Threading.Tasks;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Identity.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        public async Task HandleAsync(SignUp command)
        {
            throw new System.NotImplementedException();
        }
    }
}