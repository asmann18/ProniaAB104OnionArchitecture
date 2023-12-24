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
        CreateMap<Product, ProductGetDto>().ReverseMap();
    }
}
