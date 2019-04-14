using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Services.Carts.Dtos;
using MyShop.Services.Carts.Queries;

namespace MyShop.Services.Carts.Handlers
{
    public class GetCartHandler : IQueryHandler<GetCart, CartDto>
    {
        private readonly ICartsRepository _cartsRepository;

        public GetCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task<CartDto> HandleAsync(GetCart query)
        {
            var cart = await _cartsRepository.GetAsync(query.Id);
            if (cart is null)
            {
                throw new NotFoundException("cart_not_found",
                    $"Cart with an id: '{query.Id}' was not found.");
            }

            return new CartDto()
            {
                Id = cart.Id,
                Items = cart.Items.Select(x => new CartItemDto()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
            };
        }
    }
}