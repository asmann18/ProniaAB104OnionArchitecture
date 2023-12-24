﻿using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.ProductValidations;

public class ProductPutDtoValidation:AbstractValidator<ProductPutDto>
{
    public ProductPutDtoValidation()
    {
        RuleFor(x=>x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(64);
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0m);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.TagId).NotNull().GreaterThan(0);
        RuleFor(x => x.CategoryId).NotNull().GreaterThan(0);
    }
}
