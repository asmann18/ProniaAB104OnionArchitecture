using AutoMapper;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;

namespace Pronia.Application.AutoMappers;

public class ProductAutoMapper:Profile
{
    public ProductAutoMapper()
    {
        CreateMap<Product, ProductPostDto>().ReverseMap();
        CreateMap<Product, ProductPutDto>().ReverseMap();

        CreateMap<Product, ProductGetDto>()
                   .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.TagId).ToList())).ReverseMap();
    }
}
