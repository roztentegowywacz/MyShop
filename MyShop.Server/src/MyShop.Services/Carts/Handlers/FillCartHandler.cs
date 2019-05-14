using System.Threading.Tasks;
using MyShop.Services.Carts.Commands;
using MyShop.Services.Dispatchers;

namespace MyShop.Services.Carts.Handlers
{
    public class FillCartHandler : ICommandHandler<FillCart>
    {
        private readonly IDispatcher _dispatcher;

        public FillCartHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(FillCart command)
        {
            foreach (var product in command.CartItems)
            {
                var addProductToCart = new AddProductToCart(
                                            command.CustomerId, 
                                            product.ProductId, 
                                            product.Quantity
                                        );
                
                await _dispatcher.SendAsync(addProductToCart);
            }
        }
    }
}