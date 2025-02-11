﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.Items)
                .Must(x => x.Count > 0)
                .WithMessage("Sale should have at least one item.")
                .Must(items => items.GroupBy(i => i.Product.Id)
                                .All(g => g.Count() == 1))
                .WithMessage("Duplicated product found, items list should have unique products.");
        }
    }
}
