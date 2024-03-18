using Microsoft.AspNetCore.Mvc;
using ThesisCatalog.API.Services;

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

    [HttpGet("manufacturers")]
    public async Task<IActionResult> GetAllManufacturers()
    {
        using var logScope = _logger.BeginScope("Getting all manufacturers");
        _logger.LogInformation("Getting all manufacturers");
        var manufacturers = await _catalogService.GetAllManufacturers();
        return Ok(manufacturers);
    }
}