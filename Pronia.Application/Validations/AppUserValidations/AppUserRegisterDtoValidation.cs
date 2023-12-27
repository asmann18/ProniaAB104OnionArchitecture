using FluentValidation;
using Pronia.Application.DTOs.AppUserDtos;

namespace Pronia.Application.Validations.AppUserValidations;

public class AppUserRegisterDtoValidation:AbstractValidator<AppUserRegisterDto>
{
    public AppUserRegisterDtoValidation()
    {
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
        RuleFor(x=>x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
        RuleFor(x=>x.Fullname).NotEmpty().MinimumLength(3).MaximumLength(64);
        RuleFor(x=>x.Username).NotEmpty().MinimumLength(3).MaximumLength(64);
    }
}
