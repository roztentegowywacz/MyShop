using System;
using System.Threading.Tasks;
using Autofac;

namespace MyShop.Services.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _componentContext;

        public QueryDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _componentContext.Resolve(handlerType);

            if (handler is null)
            {
                throw new ArgumentException($"Query handler: '{handlerType.Name} was not found.'",
                    nameof(handler));
            }

            return await handler.HandleAsync((dynamic)query);
        }
    }
}