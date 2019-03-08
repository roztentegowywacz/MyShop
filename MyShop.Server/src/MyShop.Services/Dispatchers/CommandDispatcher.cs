using System;
using System.Threading.Tasks;
using Autofac;

namespace MyShop.Services.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;

        public CommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            var handler = _componentContext.Resolve<ICommandHandler<T>>();

            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name} was not found.'",
                    nameof(handler));
            }

            await handler.HandleAsync(command);
        }
    }
}