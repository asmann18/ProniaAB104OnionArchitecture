using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Helper;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs.AppUserDtos;
using Pronia.Application.Validations.CategoryValidations;
using Pronia.Domain.Entities;
using Pronia.Domain.Enums;
using Pronia.Persistence.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Pronia.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;

    public AuthService(UserManager<AppUser> userManager, IMapper mapper, ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenHelper = tokenHelper;
    }

    public async Task<AccessToken> Login(AppUserLoginDto appUserLoginDto)
    {
        AppUser user = await _userManager.FindByNameAsync(appUserLoginDto.EmailOrUsername);
        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(appUserLoginDto.EmailOrUsername);
            if (user is null)
                throw new LoginFailException();
        }
        bool result = await _userManager.CheckPasswordAsync(user, appUserLoginDto.Password);
        if (!result)
            throw new LoginFailException();

        var token = await CreateToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiredAt = token.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);
        return token;


    }

    public async Task Register(AppUserRegisterDto appUserRegisterDto)
    {
        AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == appUserRegisterDto.Username || x.Email == appUserRegisterDto.Email);
        if (user is not null)
            throw new UserAlreadyExistException();
        user = _mapper.Map<AppUser>(appUserRegisterDto);

        var success = await _userManager.CreateAsync(user, appUserRegisterDto.Password);

        if (!success.Succeeded)
        {
            StringBuilder errors = new();

            foreach (var error in success.Errors)
            {
                errors.AppendLine(error.Description);
            }
            throw new Exception(errors.ToString());
        }
        await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
        List<Claim> Claims =await ClaimsCreateAsync(user);

        await _userManager.AddClaimsAsync(user, Claims);
  
    }
    public async Task<AccessToken> RefreshTokenLogin(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (user is null)
            throw new LoginFailException();
        //if (user.RefreshTokenExpiredAt < DateTime.UtcNow)
        //    throw new LoginFailException();

        var token= await CreateToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiredAt = token.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);
        return token;

    }
    private async Task<AccessToken> CreateToken(AppUser user)
    {
        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        return _tokenHelper.CreateToken(claims);

    }

    private async Task<List<Claim>> ClaimsCreateAsync(AppUser user)
    {
        var roles=await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>() { 
        
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("Fullname",user.Fullname),
        
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim (ClaimTypes.Role,role));
        }

        return claims;
    }

}
