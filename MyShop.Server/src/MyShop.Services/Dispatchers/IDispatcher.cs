using System.Threading.Tasks;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Identity.Commands;

namespace MyShop.Services.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        // Task<TResult> aSendAsync<TResult>(ICommand<TResult> command);
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}