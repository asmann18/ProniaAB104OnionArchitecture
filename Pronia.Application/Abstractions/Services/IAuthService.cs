﻿using Pronia.Application.DTOs.AppUserDtos;
using Pronia.Application.DTOs.TokenDtos;
using Pronia.Domain.Entities;

namespace Pronia.Application.Abstractions.Services;

public interface IAuthService
{

    Task Register(AppUserRegisterDto appUserRegisterDto);
    Task<AccessToken> CreateToken(AppUser user);
    Task<AccessToken> Login(AppUserLoginDto appUserLoginDto);
}
