using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.CategoryValidations;

public class CategoryPutDtoValidation:AbstractValidator<CategoryPutDto>
{
    public CategoryPutDtoValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);

    }
}
