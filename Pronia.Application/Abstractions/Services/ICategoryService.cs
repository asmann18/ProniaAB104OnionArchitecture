using Pronia.Application.DTOs;

namespace Pronia.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<List<CategoryGetDto>> GetAllAsync(int limit, int page);
    Task<CategoryGetDto> GetAsync(int id);

    Task CreateAsync(CategoryPostDto dto);
    Task UpdateAsync(CategoryPutDto dto);
    Task DeleteAsync(int id);
}
