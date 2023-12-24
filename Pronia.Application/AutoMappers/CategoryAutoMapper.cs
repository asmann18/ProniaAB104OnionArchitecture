using AutoMapper;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;

namespace Pronia.Application.AutoMappers;

public class CategoryAutoMapper:Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<Category, CategoryPutDto>().ReverseMap();
        CreateMap<Category, CategoryPostDto>().ReverseMap();
        CreateMap<Category, CategoryRelationDto>().ReverseMap();
    }
}
