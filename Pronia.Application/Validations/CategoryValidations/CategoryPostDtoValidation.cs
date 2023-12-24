using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.CategoryValidations;

public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
{
    public CategoryPostDtoValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
