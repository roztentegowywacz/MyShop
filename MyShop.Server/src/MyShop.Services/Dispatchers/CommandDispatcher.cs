using System;
using System.Threading.Tasks;
using Autofac;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Identity.Commands;

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

        public async Task<TResult> SendAndResponseDataAsync<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult));
            
            dynamic handler = _componentContext.Resolve(handlerType);
            if (handler is null)
            {
                throw new ArgumentException($"Command handler: '{handlerType.Name} was not found.'",
                    nameof(handler));
            }

            return await handler.HandleAsync((dynamic)command);
        }
    }
}