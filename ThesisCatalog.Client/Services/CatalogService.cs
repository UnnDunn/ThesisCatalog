using System.Text.Json;
using Microsoft.FluentUI.AspNetCore.Components;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.Client.Services;

public class CatalogService
{
    private List<ComponentManufacturer> Manufacturers { get; set; }
    private List<ComputerCatalogItem> CatalogItems { get; set; }

    private readonly ILogger<CatalogService> _logger;

    // private ThesisHttpClientFactory _httpClientFactory;

    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
    {
        // _httpClientFactory = httpClientFactory;
        _httpClient = httpClient;
        _logger = logger;
        Manufacturers = new List<ComponentManufacturer>();
        CatalogItems = new List<ComputerCatalogItem>();
        
        // seed design-time data
        // SeedDesignTimeData();
    }

    private void SeedDesignTimeData()
    {
        // Add manufacturers
        Manufacturers =
        [
            new ComponentManufacturer { Id = 1, Name = "Intel", ComponentType = ComponentType.Cpu | ComponentType.Gpu },
            new ComponentManufacturer { Id = 2, Name = "AMD", ComponentType = ComponentType.Cpu | ComponentType.Gpu },
            new ComponentManufacturer { Id = 3, Name = "NVIDIA", ComponentType = ComponentType.Gpu },
            new ComponentManufacturer { Id = 4, Name = "Qualcomm", ComponentType = ComponentType.Cpu }
        ];

        var manufacturers = Manufacturers.ToDictionary(m => m.Id);

        // Add catalog items
        CatalogItems =
        [
            new ComputerCatalogItem
            {
                Id = 1,
                Memory = new MemorySpecification(8, StorageUnit.GB),
                StorageSpecification = new StorageSpecification(1, StorageUnit.TB, StorageType.SSD),
                UsbSpecification = new UsbSpecification(new Dictionary<UsbType, short>
                {
                    { UsbType.USB3, 2 },
                    { UsbType.USB2, 4 }
                }),
                GpuDescriptor = new ComponentDescriptor
                    { Manufacturer = manufacturers[3], ModelName = "GeForce GTX 770" },
                Weight = new Weight(8100, WeightUnit.Grams),
                PsuRating = 500,
                CpuDescriptor = new ComponentDescriptor
                    { Manufacturer = manufacturers[1], ModelName = "Celeron N3050" }
            },

            new ComputerCatalogItem
            {
                Id = 2,
                Memory = new MemorySpecification(16, StorageUnit.GB),
                StorageSpecification = new StorageSpecification(2, StorageUnit.TB, StorageType.HDD),
                UsbSpecification = new UsbSpecification(new Dictionary<UsbType, short>
                {
                    { UsbType.USB3, 3 },
                    { UsbType.USB2, 4 }
                }),
                GpuDescriptor = new ComponentDescriptor
                {
                    Manufacturer = manufacturers[3],
                    ModelName = "GeForce GTX 960"
                },
                Weight = new Weight(12, WeightUnit.Kilograms),
                PsuRating = 500,
                CpuDescriptor = new ComponentDescriptor
                {
                    Manufacturer = manufacturers[2],
                    ModelName = "FX 4300"
                }
            },

            new ComputerCatalogItem
            {
                Id = 3,
                Memory = new MemorySpecification(16, StorageUnit.GB),
                StorageSpecification = new StorageSpecification(3, StorageUnit.TB, StorageType.HDD),
                UsbSpecification = new UsbSpecification(new Dictionary<UsbType, short>
                {
                    { UsbType.USB3, 4 },
                    { UsbType.USB2, 4 }
                }),
                GpuDescriptor = new ComponentDescriptor
                {
                    Manufacturer = manufacturers[2],
                    ModelName = "Radeon R7360"
                },
                Weight = new Weight(16, WeightUnit.Pounds),
                PsuRating = 450,
                CpuDescriptor = new ComponentDescriptor
                {
                    Manufacturer = manufacturers[2],
                    ModelName = "Athlon 5150 Quad-Core"
                }
            }
        ];
    }

    public Dictionary<int, ComputerCatalogItem> GetAllCatalogItems()
    {
        var result = CatalogItems.ToDictionary(c => c.Id);
        return result;
    }

    public Dictionary<int, ComponentManufacturer> GetAllComponentManufacturers(
        ComponentType? componentTypeFilter = null)
    {
        return componentTypeFilter is null
            ? Manufacturers.ToDictionary(m => m.Id)
            : Manufacturers
                .Where(m => (m.ComponentType & componentTypeFilter.Value) > 0)
                .ToDictionary(m => m.Id);
    }

    public async Task<Dictionary<int, ComputerCatalogItem>> GetAllCatalogItemsAsync()
    {
        // using var http = _httpClientFactory.CreateClient();
        using var logScope = _logger.BeginScope("Getting catalog items from API");
        var response = await _httpClient.GetAsync("api/Catalog/items");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var catalogItems = JsonSerializer.Deserialize(content, ThesisCatalogJsonSerializerContext.Default.ListComputerCatalogItem);
            _logger.LogInformation("{catalogItemCount} items retrieved", catalogItems?.Count ?? 0);
            return catalogItems?.ToDictionary(c => c.Id) ?? new Dictionary<int, ComputerCatalogItem>();
        }
        else
        {
            throw new Exception("Failed to retrieve catalog items.");
        }
    }

    public async Task<Dictionary<int, ComponentManufacturer>> GetAllComponentManufacturersAsync()
    {
        if (Manufacturers.Count != 0) return Manufacturers.ToDictionary(m => m.Id);

        using var logScope = _logger.BeginScope("Getting component manufacturers from API");
        var response = await _httpClient.GetAsync("api/Catalog/manufacturers");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var manufacturers = JsonSerializer.Deserialize(content,
                ThesisCatalogJsonSerializerContext.Default.ListComponentManufacturer);
            _logger.LogInformation("{manufacturerCount} manufacturers retrieved", manufacturers?.Count ?? 0);
            Manufacturers.Clear();
            Manufacturers.AddRange(manufacturers ?? []);
            return Manufacturers.ToDictionary(m => m.Id);
        }
        else
        {
            throw new Exception("Failed to retrieve component manufacturers.");
        }
    }
}