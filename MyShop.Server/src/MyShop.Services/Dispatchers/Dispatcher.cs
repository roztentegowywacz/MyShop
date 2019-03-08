using System.Threading.Tasks;

namespace MyShop.Services.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispacher;

        public Dispatcher(ICommandDispatcher commandDispatcher,
                            IQueryDispatcher queryDispacher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispacher = queryDispacher;
        }
        
        public async Task<Tresult> QueryAsync<Tresult>(IQuery<Tresult> query)
            => await _queryDispacher.QueryAsync<Tresult>(query);

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
            => await _commandDispatcher.SendAsync(command);
    }
}