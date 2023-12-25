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
    ITagService _tagService;
    public ProductService(IProductRepository repository, IMapper mapper, ITagService tagService)
    {
        _repository = repository;
        _mapper = mapper;
        _tagService = tagService;
    }

    public async Task CreateAsync(ProductPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new ProductAlreadyExistException();

        var product = _mapper.Map<Product>(dto);
        product.ProductTags = new List<ProductTag>();
        foreach (var tagId in dto.TagIds)
        {
            if (!(await _tagService.IsExistAsync(tagId)))
                throw new TagNotFoundException();
            product.ProductTags.Add(new() { TagId=tagId });
            
        }
        await _repository.CreateAsync(product);
        await _repository.SaveAsync();
    }

 

    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _repository.GetAll(includes: new string[] { "Category", "ProductTags.Tag" }).ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return dtos;
    }

    public async Task<List<ProductGetDto>> GetAllAsync(int limit, int page)
    {
        var products = await _repository.Paginate(_repository.OrderBy(_repository.GetAll(includes:new string[] { "Category", "ProductTags.Tag"}), x => x.CreatedBy), limit, page).ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return dtos;
    }
    public async Task<List<ProductGetDto>> GetDeletedProducts()
    {
        var products = await _repository.GetFiltered(x => x.IsDeleted == true, true, "Category", "Tag").ToListAsync();
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

    public async Task HardDeleteAsync(int id)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id);
        if (product is null)
            throw new ProductNotFoundException();
        _repository.HardDelete(product);
        await _repository.SaveAsync();
    }

    public async Task RepairDeleteAsync(int id)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id,true);
        if (product is null)
            throw new ProductNotFoundException();
        _repository.Repair(product);
        await _repository.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var product = await _repository.GetSingleAsync(x => x.Id == id);
        if (product is null)
            throw new ProductNotFoundException();
        _repository.SoftDelete(product);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(ProductPutDto dto)
    {
        var existed = await _repository.GetSingleAsync(x => x.Id == dto.Id,includes: new string[] {"Category","ProductTags.Tag"});
        if (existed is null)
            throw new ProductNotFoundException();
        var isExisted = await _repository.IsExistAsync(x => x.Name == dto.Name && x.Id != dto.Id);
        if (isExisted)
            throw new ProductAlreadyExistException();
        if (existed.ProductTags is null)
            existed.ProductTags = new List<ProductTag>();
        existed.ProductTags = existed.ProductTags.Where(x => dto.TagIds.Any(i => i == x.TagId)).ToList();
        existed = _mapper.Map(dto, existed);
        foreach (var tagId in dto.TagIds)
        {
            if (!(await _tagService.IsExistAsync(tagId)))
                throw new TagNotFoundException();

            if (!existed.ProductTags.Any(x => x.TagId == tagId))
                existed.ProductTags.Add(new() { TagId = tagId });
        }
        _repository.Update(existed);
        await _repository.SaveAsync();
    }
}
