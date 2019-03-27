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

        // TODO: Add nullcheck if command not found
        // public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        // {
        //     var handlerType = typeof(ICommandHandler<,>)
        //         .MakeGenericType(command.GetType(), typeof(TResult));
            
        //     dynamic handler = _componentContext.Resolve(handlerType);

        //     return await handler.HandleAsync((dynamic)command);
        // }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(typeof(TCommand), typeof(TResult));
            
            dynamic handler = _componentContext.Resolve(handlerType);

            return await handler.HandleAsync((dynamic)command);
        }

        // public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        // {
        //     var handlerType = typeof(ICommandHandler<,>)
        //         .MakeGenericType(command.GetType(), typeof(TResult));
            
        //     dynamic handler = _componentContext.Resolve(handlerType);

        //     return await handler.HandleAsync((dynamic)command);
        // }
    }
}