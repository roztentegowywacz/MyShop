using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
        Task<TResult> SendAndResponseDataAsync<TResult>(ICommand<TResult> command);
    }
}