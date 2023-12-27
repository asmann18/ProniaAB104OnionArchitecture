using AutoMapper;
using Pronia.Application.DTOs.AppUserDtos;
using Pronia.Domain.Entities;

namespace Pronia.Application.AutoMappers;

public class AppUserAutoMapper:Profile
{
    public AppUserAutoMapper()
    {
        CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();   
        CreateMap<AppUser, AppUserGetDto>().ReverseMap();   
    }
}
