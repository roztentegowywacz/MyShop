using System.Threading.Tasks;
using MyShop.Services.Carts.Commands.AddProductToCart;
using MyShop.Services.Dispatchers;

namespace MyShop.Services.Carts.Commands.FillCart
{
    public class FillCartHandler : ICommandHandler<FillCartCommand>
    {
        private readonly IDispatcher _dispatcher;

        public FillCartHandler(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(FillCartCommand command)
        {
            foreach (var product in command.CartItems)
            {
                var addProductToCart = new AddProductToCartCommand(
                                            command.CustomerId, 
                                            product.ProductId, 
                                            product.Quantity
                                        );
                
                await _dispatcher.SendAsync(addProductToCart);
            }
        }
    }
}