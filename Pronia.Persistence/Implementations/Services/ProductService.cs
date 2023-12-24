using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;
using Pronia.Persistence.Exceptions;

namespace Pronia.Persistence.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(ProductPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new ProductAlreadyExistException();

        var product = _mapper.Map<Product>(dto);
        await _repository.CreateAsync(product);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id);
        if (product is null)
            throw new ProductNotFoundException();
        _repository.Delete(product);
        await _repository.SaveAsync();

    }

    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _repository.GetAll("Category").ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return dtos;
    }

    public async Task<List<ProductGetDto>> GetAllAsync(int limit, int page)
    {
        var products = await _repository.Paginate(_repository.OrderBy(_repository.GetAll( "Category", "Tag"), x => x.CreatedBy), limit, page).ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return dtos;
    }

    public async Task<ProductGetDto> GetAsync(int id)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id);
        if (product is null)
            throw new ProductNotFoundException();
        var dto = _mapper.Map<ProductGetDto>(product);
        return dto;
    }

    public async Task UpdateAsync(ProductPutDto dto)
    {
        var existed = await _repository.GetSingleAsync(x => x.Id == dto.Id);
        if (existed is null)
            throw new ProductNotFoundException();
        var isExisted = await _repository.IsExistAsync(x => x.Name == dto.Name && x.Id != dto.Id);
        if (isExisted)
            throw new ProductAlreadyExistException();
        existed = _mapper.Map(dto, existed);
    }
}
