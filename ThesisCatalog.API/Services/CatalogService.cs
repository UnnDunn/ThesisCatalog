using Microsoft.EntityFrameworkCore;
using ThesisCatalog.API.Data;
using ThesisCatalog.API.Data.Entities;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Services;

public class CatalogService
{
    private readonly ThesisCatalogDbContext _dbContext;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(ThesisCatalogDbContext dbContext, ILogger<CatalogService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<ComputerCatalogItem>> GetAllCatalogItems()
    {
        var items = await _dbContext.CatalogItems
            .Include(i => i.Weight)
            .Include(i => i.MemorySpecification)
            .Include(i => i.StorageSpecification)
            .Include(i => i.UsbSpecifications)
            .Include(i => i.CpuDescriptor.Manufacturer)
            .Include(i => i.GpuDescriptor.Manufacturer)
            .ToListAsync();
        
        return items.Select(item => (ComputerCatalogItem)item)
            .ToList();
    }

    public async Task<List<ComponentManufacturer>> GetAllManufacturers()
    {
        var manufacturers = await _dbContext.Manufacturers.ToListAsync();
        return manufacturers.Select(m => new ComponentManufacturer
        {
            Id = m.Id, Name = m.Name, ComponentType = m.ComponentTypes
        }).ToList();
    }

    public async Task<ComputerCatalogItem> AddCatalogItem(ComputerCatalogItem item)
    {
        var dbCatalogItem = (CatalogItem)item;
        _dbContext.CatalogItems.Add(dbCatalogItem);
        await _dbContext.SaveChangesAsync();
        
        return (ComputerCatalogItem)dbCatalogItem;
    }

    public async Task<int> AddCatalogItems(IEnumerable<ComputerCatalogItem> items)
    {
        var dbCatalogItems = items.Select(item => (CatalogItem)item).ToList();
        _dbContext.CatalogItems.AddRange(dbCatalogItems);
        var rowCount = await _dbContext.SaveChangesAsync();
        return rowCount;
    }

    public async Task<ComponentManufacturer> AddManufacturer(ComponentManufacturer manufacturer)
    {
        var dbManufacturer = (Manufacturer)manufacturer;
        _dbContext.Manufacturers.Add(dbManufacturer);
        await _dbContext.SaveChangesAsync();

        return (ComponentManufacturer)dbManufacturer;
    }

    public async Task SeedInitialData()
    {
        var manufacturers = new List<Manufacturer>();
        var manufacturerCount = await _dbContext.Manufacturers.CountAsync();
        if (manufacturerCount == 0)
        {
            manufacturers =
            [
                new Manufacturer { Name = "Intel", ComponentTypes = ComponentType.Cpu | ComponentType.Gpu },
                new Manufacturer { Name = "AMD", ComponentTypes = ComponentType.Gpu | ComponentType.Gpu },
                new Manufacturer { Name = "NVIDIA", ComponentTypes = ComponentType.Gpu },
                new Manufacturer { Name = "Qualcomm", ComponentTypes = ComponentType.Cpu }
            ];
            _dbContext.Manufacturers.AddRange(manufacturers);
            await _dbContext.SaveChangesAsync();
        }

        var manufacturerDictionary = manufacturers.ToDictionary(m => m.Name);

        var catalogItemCount = await _dbContext.CatalogItems.CountAsync();

        if (catalogItemCount > 0) return;

        var catalogItems = new List<CatalogItem>
        {
            new()
            {
                MemorySpecification =
                    new Data.Entities.MemorySpecification
                    {
                        MemoryBytes = (long)8 * 1024 * 1024 * 1024, MemoryDisplayUnit = StorageUnit.GB
                    },
                StorageSpecification =
                    new Data.Entities.StorageSpecification
                    {
                        StorageBytes = (long)512 * 1024 * 1024 * 1024,
                        StorageDisplayUnit = StorageUnit.GB,
                        StorageType = StorageType.SSD
                    },
                PsuRating = 650,
                Weight =
                    new WeightSpecification { WeightGrams = 2000, WeightDisplayUnit = WeightUnit.Grams },
                CpuDescriptor =
                    new Data.Entities.ComponentDescriptor
                    {
                        ManufacturerId = manufacturerDictionary["Intel"].Id,
                        ModelName = "Core i7-10700K"
                    },
                GpuDescriptor =
                    new Data.Entities.ComponentDescriptor
                    {
                        ManufacturerId = manufacturerDictionary["NVIDIA"].Id,
                        ModelName = "GeForce RTX 3080"
                    },
                UsbSpecifications =
                    new List<Data.Entities.UsbSpecification>
                    {
                        new() { UsbType = UsbType.USB2, PortCount = 4 },
                        new() { UsbType = UsbType.USB3, PortCount = 2 }
                    }
            },
            new()
            {
                MemorySpecification =
                    new Data.Entities.MemorySpecification
                    {
                        MemoryBytes = (long)16 * 1024 * 1024 * 1024, MemoryDisplayUnit = StorageUnit.GB
                    },
                StorageSpecification =
                    new Data.Entities.StorageSpecification
                    {
                        StorageBytes = (long)1 * 1024 * 1024 * 1024 * 1024,
                        StorageDisplayUnit = StorageUnit.TB,
                        StorageType = StorageType.HDD
                    },
                PsuRating = 500,
                Weight =
                    new WeightSpecification { WeightGrams = 1500, WeightDisplayUnit = WeightUnit.Grams },
                CpuDescriptor =
                    new Data.Entities.ComponentDescriptor
                    {
                        ManufacturerId = manufacturerDictionary["AMD"].Id,
                        ModelName = "Ryzen 7 5800X"
                    },
                GpuDescriptor =
                    new Data.Entities.ComponentDescriptor
                    {
                        ManufacturerId = manufacturerDictionary["AMD"].Id,
                        ModelName = "Radeon RX 6800 XT"
                    },
                UsbSpecifications = new List<Data.Entities.UsbSpecification>
                {
                    new() { UsbType = UsbType.USB2, PortCount = 2 },
                    new() { UsbType = UsbType.USB3, PortCount = 2 },
                    new() { UsbType = UsbType.USBC, PortCount = 1 }
                }
            }
        };
        _dbContext.CatalogItems.AddRange(catalogItems);
        await _dbContext.SaveChangesAsync();
    }
}