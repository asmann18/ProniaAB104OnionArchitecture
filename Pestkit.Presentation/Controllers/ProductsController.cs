using Microsoft.AspNetCore.Mvc;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs;

namespace Pronia.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {

        return StatusCode(StatusCodes.Status200OK, await _service.GetAllAsync());
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetDeletedProducts()
    {

        return StatusCode(StatusCodes.Status200OK, await _service.GetDeletedProducts());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
    }
    [HttpDelete]
    [Route("softDelete/{id}")]
    public async Task<IActionResult> SoftDeleteById([FromRoute] int id)
    {
        await _service.SoftDeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    [HttpDelete]
    [Route("hardDelete/{id}")]
    public async Task<IActionResult> HardDeleteById([FromRoute] int id)
    {
        await _service.HardDeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    [HttpDelete]
    [Route("repairDelete/{id}")]
    public async Task<IActionResult> RepairDeleteById([FromRoute] int id)
    {
        await _service.RepairDeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] ProductPutDto productPutDto)
    {
        await _service.UpdateAsync(productPutDto);
        return StatusCode(StatusCodes.Status204NoContent);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductPostDto productPostDto)
    {
        await _service.CreateAsync(productPostDto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
