using System.Threading.Tasks;

namespace MyShop.Services
{
    public interface ICommandHandler<in T>  where T : ICommand
    {
        Task HandleAsync(T command);
    }

    public interface ICommandHandler<T, TResult> where T : ICommand<TResult>
    {
        Task<TResult> HandleAsync(T command);
    }
}