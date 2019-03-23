using System.Threading.Tasks;

namespace MyShop.Services
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);       
    }
}