using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.TagValidations;

public class TagPostDtoValidation:AbstractValidator<TagPostDto>
{
    public TagPostDtoValidation()
    {
        RuleFor(x=>x.Name).NotEmpty().MaximumLength(64).MinimumLength(1);
    }
}
