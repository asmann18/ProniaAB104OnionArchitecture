using Pronia.Application.DTOs;

namespace Pronia.Application.Abstractions.Services;

public interface ITagService
{
    Task<List<TagGetDto>> GetAllAsync();
    Task<List<TagGetDto>> GetAllAsync(int limit, int page);
    Task<TagGetDto> GetAsync(int id);

    Task CreateAsync(TagPostDto dto);
    Task UpdateAsync(TagPutDto dto);
    Task DeleteAsync(int id);
    Task<bool> IsExistAsync(int id);
}
