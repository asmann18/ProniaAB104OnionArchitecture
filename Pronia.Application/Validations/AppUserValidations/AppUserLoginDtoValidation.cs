using FluentValidation;
using Pronia.Application.DTOs.AppUserDtos;

namespace Pronia.Application.Validations.AppUserValidations;

public class AppUserLoginDtoValidation:AbstractValidator<AppUserLoginDto>
{
    public AppUserLoginDtoValidation()
    {
        RuleFor(x=>x.EmailOrUsername).NotEmpty().MinimumLength(3).MaximumLength(256);
        RuleFor(x=>x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
    }
}
