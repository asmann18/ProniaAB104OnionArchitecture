using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.ProductValidations;

public class ProductPostDtoValidation : AbstractValidator<ProductPostDto>
{
    public ProductPostDtoValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(64);
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0m);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleForEach(x => x.TagIds)
                   .NotEmpty().WithMessage("TagId should not be empty")
                   .GreaterThanOrEqualTo(0).WithMessage("TagId should be greater than or equal to 0");
        RuleFor(x => x.CategoryId).NotNull().GreaterThan(0);
    }
}
