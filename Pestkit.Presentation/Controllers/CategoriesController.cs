using Microsoft.AspNetCore.Mvc;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs;

namespace Pestkit.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {

        return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] CategoryPutDto categoryPutDto)
    {
        await _service.UpdateAsync(categoryPutDto);
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryPostDto categoryPostDto)
    {
        await _service.CreateAsync(categoryPostDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
