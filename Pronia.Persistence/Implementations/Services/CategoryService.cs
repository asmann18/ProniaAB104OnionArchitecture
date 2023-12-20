using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;
using Pronia.Persistence.Exceptions;

namespace Pronia.Persistence.Implementations.Services;

public class CategoryService:ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CategoryPostDto dto)
    {
        bool isExist = await _categoryRepository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new CategoryAlreadyExistException();
        var category = _mapper.Map<Category>(dto);
        await _categoryRepository.CreateAsync(category);
        await _categoryRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetSingleAsync(x => x.Id == id);
        if (category is null)
            throw new CategoryNotFoundException();

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.OrderBy((_categoryRepository.GetAllAsync()), x => x.Id).ToListAsync();
        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);
        return dtos;
    }

    public async Task<List<CategoryGetDto>> GetAllAsync(int limit, int page)
    {
        var categories = await _categoryRepository.Paginate((_categoryRepository.GetAllAsync()), limit, page).ToListAsync();
        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);
        return dtos;
    }

    public async Task<CategoryGetDto> GetAsync(int id)
    {
        var category = await _categoryRepository.GetSingleAsync(x => x.Id == id);
        if (category is null)
            throw new CategoryNotFoundException();
        var dto = _mapper.Map<CategoryGetDto>(category);
        return dto;
    }

    public async Task UpdateAsync(CategoryPutDto dto)
    {
        var existedCategory = await _categoryRepository.GetSingleAsync(x => x.Id == dto.Id);
        if (existedCategory is null)
            throw new CategoryNotFoundException();

        bool isExist = await _categoryRepository.IsExistAsync(x => x.Name == dto.Name && x.Id != dto.Id);
        if (isExist)
            throw new CategoryAlreadyExistException();

        existedCategory = _mapper.Map<CategoryPutDto, Category>(dto, existedCategory);
        _categoryRepository.Update(existedCategory);
        await _categoryRepository.SaveAsync();
    }

}
