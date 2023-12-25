using Pronia.Application.DTOs;

namespace Pronia.Application.Abstractions.Services;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllAsync();
    Task<List<ProductGetDto>> GetAllAsync(int limit, int page);
    Task<List<ProductGetDto>> GetDeletedProducts();
    Task<ProductGetDto> GetAsync(int id);

    Task CreateAsync(ProductPostDto dto);
    Task UpdateAsync(ProductPutDto dto);
    Task SoftDeleteAsync(int id);
    Task HardDeleteAsync(int id);
    Task RepairDeleteAsync(int id);
}
