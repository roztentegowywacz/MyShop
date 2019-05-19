using FluentValidation;
using MyShop.Core.Domain.Products;

namespace MyShop.Services.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).Length(2, 3);
        }
    }
}