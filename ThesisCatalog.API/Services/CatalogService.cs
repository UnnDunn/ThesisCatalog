using Microsoft.EntityFrameworkCore;
using ThesisCatalog.API.Data;
using ThesisCatalog.API.Data.Entities;
using ThesisCatalog.API.Exceptions;
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
        using var logScope = _logger.BeginScope("Getting all catalog items");
        var items = await _dbContext.CatalogItems
            .Include(i => i.Weight)
            .Include(i => i.MemorySpecification)
            .Include(i => i.StorageSpecification)
            .Include(i => i.UsbSpecifications)
            .Include(i => i.CpuDescriptor.Manufacturer)
            .Include(i => i.GpuDescriptor.Manufacturer)
            .AsNoTracking()
            .ToListAsync();
        
        _logger.LogInformation("{itemCount} items found", items.Count);
        return items.Select(item => (ComputerCatalogItem)item)
            .ToList();
    }

    public async Task<List<ComputerCatalogItem>> GetCatalogItemsBySearchText(string searchText)
    {
        using var logScope = _logger.BeginScope("Getting catalog items matching search text {searchText}", searchText);
        var items = await _dbContext.CatalogItems
            .Include(i => i.Weight)
            .Include(i => i.MemorySpecification)
            .Include(i => i.StorageSpecification)
            .Include(i => i.UsbSpecifications)
            .Include(i => i.CpuDescriptor.Manufacturer)
            .Include(i => i.GpuDescriptor.Manufacturer)
            .Where(i => i.SearchText.Contains(searchText))
            .AsNoTracking()
            .ToListAsync();
        _logger.LogInformation("{itemCount} items found", items.Count);
        return items.Select(item => (ComputerCatalogItem)item).ToList();
    }

    public async Task<List<ComponentManufacturer>> GetAllManufacturers()
    {
        using var logScope = _logger.BeginScope("Getting all manufacturers");
        var manufacturers = await _dbContext.Manufacturers.AsNoTracking().ToListAsync();
        _logger.LogInformation("{manufacturerCount} manufacturers found", manufacturers.Count);
        return manufacturers.Select(m => new ComponentManufacturer
        {
            Id = m.Id, Name = m.Name, ComponentType = m.ComponentTypes
        }).ToList();
    }
    
    public async Task<ComputerCatalogItem> GetCatalogItemById(int id)
    {
        try
        {
            var catalogItem = await _dbContext.CatalogItems
                .Include(i => i.Weight)
                .Include(i => i.MemorySpecification)
                .Include(i => i.StorageSpecification)
                .Include(i => i.UsbSpecifications)
                .Include(i => i.CpuDescriptor.Manufacturer)
                .Include(i => i.GpuDescriptor.Manufacturer)
                .FirstAsync(i => i.Id == id);
            return (ComputerCatalogItem)catalogItem;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Invalid operation exception getting catalog item with id {id}", id);
            throw new NotFoundException($"Catalog item with id {id} was not found", ex);
        }
    }
    
    

    public async Task EditCatalogItem(int id, ComputerCatalogItem editedItem)
    {
        var existingItem = await _dbContext.CatalogItems
            .Include(i => i.Weight)
            .Include(i => i.MemorySpecification)
            .Include(i => i.StorageSpecification)
            .Include(i => i.UsbSpecifications)
            .Include(i => i.CpuDescriptor.Manufacturer)
            .Include(i => i.GpuDescriptor.Manufacturer)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (existingItem == null)
        {
            throw new NotFoundException($"Catalog item with ID {id} not found");
        }

        var editedDataItem = (CatalogItem)editedItem;
        existingItem.MemorySpecification.MemoryBytes = editedDataItem.MemorySpecification.MemoryBytes;
        existingItem.MemorySpecification.MemoryDisplayUnit = editedDataItem.MemorySpecification.MemoryDisplayUnit;
        
        existingItem.StorageSpecification.StorageBytes = editedDataItem.StorageSpecification.StorageBytes;
        existingItem.StorageSpecification.StorageDisplayUnit = editedDataItem.StorageSpecification.StorageDisplayUnit;
        existingItem.StorageSpecification.StorageType = editedDataItem.StorageSpecification.StorageType;
        
        existingItem.PsuRating = editedDataItem.PsuRating;
        
        existingItem.Weight.WeightGrams = editedDataItem.Weight.WeightGrams;
        existingItem.Weight.WeightDisplayUnit = editedDataItem.Weight.WeightDisplayUnit;
        
        existingItem.CpuDescriptor.ManufacturerId = editedDataItem.CpuDescriptor.ManufacturerId;
        existingItem.CpuDescriptor.ModelName = editedDataItem.CpuDescriptor.ModelName;
        
        existingItem.GpuDescriptor.ManufacturerId = editedDataItem.GpuDescriptor.ManufacturerId;
        existingItem.GpuDescriptor.ModelName = editedDataItem.GpuDescriptor.ModelName;

        existingItem.SearchText = editedDataItem.SearchText;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> RefreshAllCatalogItems()
    {
        var catalogItems = await _dbContext.CatalogItems
            .Include(i => i.Weight)
            .Include(i => i.MemorySpecification)
            .Include(i => i.StorageSpecification)
            .Include(i => i.UsbSpecifications)
            .Include(i => i.CpuDescriptor.Manufacturer)
            .Include(i => i.GpuDescriptor.Manufacturer)
            .ToListAsync();
        var entityEntries = _dbContext.ChangeTracker.Entries<CatalogItem>();
        foreach (var entry in entityEntries)
        {
            entry.State = EntityState.Modified;
        }

        var rowCount = await _dbContext.SaveChangesAsync();
        return rowCount;
    }

    public async Task<ComputerCatalogItem> AddCatalogItem(ComputerCatalogItem item)
    {
        var dbCatalogItem = (CatalogItem)item;

        var manufacturers = await _dbContext.Manufacturers.ToDictionaryAsync(m => m.Id);
        
        _dbContext.CatalogItems.Add(dbCatalogItem);
        await _dbContext.SaveChangesAsync();
        
        return (ComputerCatalogItem)dbCatalogItem;
    }

    public async Task RemoveCatalogItem(int id)
    {
        var catalogItem = await _dbContext.CatalogItems.FindAsync(id);
        if (catalogItem is null)
        {
            throw new NotFoundException($"Catalog item with ID {id} not found");
        }
        _dbContext.CatalogItems.Remove(catalogItem);
        await _dbContext.SaveChangesAsync();
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
                new Manufacturer { Name = "AMD", ComponentTypes = ComponentType.Cpu | ComponentType.Gpu },
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