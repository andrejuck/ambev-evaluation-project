using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.Items)
               .NotNull()
               .WithMessage("Sale should have at least one item.")
               .Must(items => items.GroupBy(i => i.Product.Id)
                               .All(g => g.Count() == 1))
               .WithMessage("Duplicated product found, items list should have unique products.");
        }
    }
}
