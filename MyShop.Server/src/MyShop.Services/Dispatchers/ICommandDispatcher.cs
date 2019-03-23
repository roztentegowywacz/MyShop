using System.Threading.Tasks;

namespace MyShop.Services.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}