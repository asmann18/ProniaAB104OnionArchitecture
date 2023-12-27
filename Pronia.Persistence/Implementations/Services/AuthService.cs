using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Helper;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs.AppUserDtos;
using Pronia.Application.DTOs.TokenDtos;
using Pronia.Domain.Entities;
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


        return await CreateToken(user);


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

        List<Claim> Claims = ClaimsCreate(user);

        await _userManager.AddClaimsAsync(user, Claims);



    }
    public async Task<AccessToken> CreateToken(AppUser user)
    {
        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        return _tokenHelper.CreateToken(claims);

    }

    private static List<Claim> ClaimsCreate(AppUser user)
    {
        return new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("Fullname",user.Fullname)

        };
    }

}
