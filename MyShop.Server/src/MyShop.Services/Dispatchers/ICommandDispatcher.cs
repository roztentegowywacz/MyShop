using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
        // Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
    }
}