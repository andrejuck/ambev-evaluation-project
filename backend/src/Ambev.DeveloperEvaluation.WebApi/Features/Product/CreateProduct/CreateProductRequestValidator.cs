﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
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
