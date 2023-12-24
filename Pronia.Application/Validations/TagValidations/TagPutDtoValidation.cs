using FluentValidation;
using Pronia.Application.DTOs;

namespace Pronia.Application.Validations.TagValidations;

public class TagPutDtoValidation:AbstractValidator<TagPutDto>
{
    public TagPutDtoValidation()
    {
        RuleFor(x=>x.Name).NotEmpty().MaximumLength(64);
    }
}
