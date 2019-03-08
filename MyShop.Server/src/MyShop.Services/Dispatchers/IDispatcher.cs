using System.Threading.Tasks;

namespace MyShop.Services.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<Tresult> QueryAsync<Tresult>(IQuery<Tresult> query);
    }
}