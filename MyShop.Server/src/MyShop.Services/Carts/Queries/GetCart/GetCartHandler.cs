using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Carts.Repositories;
using MyShop.Core.Domain.Exceptions;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Carts.Dtos;

namespace MyShop.Services.Carts.Queries.GetCart
{
    public class GetCartHandler : IQueryHandler<GetCartQuery, CartDto>
    {
        private readonly ICartsRepository _cartsRepository;

        public GetCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task<CartDto> HandleAsync(GetCartQuery query)
        {
            var cart = await _cartsRepository.GetAsync(query.Id);
            cart.NullCheck(ErrorCodes.cart_not_found, query.Id);

            return new CartDto()
            {
                Id = cart.Id,
                Items = cart.Items.Select(x => new CartItemDto()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                })
            };
        }
    }
}