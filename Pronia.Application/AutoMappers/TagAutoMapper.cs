using AutoMapper;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;

namespace Pronia.Application.AutoMappers;

public class TagAutoMapper:Profile
{
    public TagAutoMapper()
    {
        CreateMap<Tag, TagPostDto>().ReverseMap();
        CreateMap<Tag, TagPutDto>().ReverseMap();
        CreateMap<Tag, TagGetDto>().ReverseMap();
        CreateMap<Tag, TagRelationDto>().ReverseMap();
    }
}
