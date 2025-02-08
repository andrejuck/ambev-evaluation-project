using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.Quantity)
                .LessThanOrEqualTo(20)
                .GreaterThan(0)
                .WithMessage("Item quantity should be between 1 and 20.");

            RuleFor(item => item.Product)
                .SetValidator(new ProductValidator());
        }
    }
}
