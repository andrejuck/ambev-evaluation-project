
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Items)
                .NotNull()
                .WithMessage("Sale should have at least one item.")
                .Must(items => items.GroupBy(i => i.Product)
                                .All(g => g.Count() == 1))
                .WithMessage("Duplicated product found, items list should have unique products.");

            RuleForEach(sale => sale.Items).SetValidator(new SaleItemValidator());
        }
    }
}
