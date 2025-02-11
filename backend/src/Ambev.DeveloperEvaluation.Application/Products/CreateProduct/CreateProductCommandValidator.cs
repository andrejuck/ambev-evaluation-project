
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Product name should not be empty.");

            RuleFor(product => product.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Product unit price should be higher than zero.");
        }
    }
}
