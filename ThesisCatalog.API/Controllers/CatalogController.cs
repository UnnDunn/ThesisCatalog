using Microsoft.AspNetCore.Mvc;
using ThesisCatalog.API.Exceptions;
using ThesisCatalog.API.Services;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private CatalogService _catalogService;
    private ILogger<CatalogController> _logger;

    public CatalogController(CatalogService catalogService, ILogger<CatalogController> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }
    
    [HttpGet("items")]
    public async Task<IActionResult> GetAllCatalogItems()
    {
        using var logScope = _logger.BeginScope("Getting all catalog items");
        _logger.LogInformation("Getting all catalog items");
        var items = await _catalogService.GetAllCatalogItems();
        return Ok(items);
    }

    [HttpPut("items/{id:int}")]
    public async Task<IActionResult> EditCatalogItem(int id, [FromBody] ComputerCatalogItem item)
    {
        try
        {
            await _catalogService.EditCatalogItem(id, item);
            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("items/{id:int}")]
    public async Task<IActionResult> DeleteCatalogItem(int id)
    {
        try
        {
            await _catalogService.RemoveCatalogItem(id);
            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddCatalogItem([FromBody] ComputerCatalogItem item)
    {
        var addedItem = await _catalogService.AddCatalogItem(item);
        return Ok(addedItem);
    }

    [HttpGet("manufacturers")]
    public async Task<IActionResult> GetAllManufacturers()
    {
        using var logScope = _logger.BeginScope("Getting all manufacturers");
        _logger.LogInformation("Getting all manufacturers");
        var manufacturers = await _catalogService.GetAllManufacturers();
        return Ok(manufacturers);
    }
}