using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.Client.Services;

public class CatalogService
{
    private Dictionary<int, ComponentManufacturer> Manufacturers { get; set; }
    private Dictionary<int, ComputerCatalogItem> CatalogItems { get; set; }

    public CatalogService()
    {
        Manufacturers = new Dictionary<int, ComponentManufacturer>();
        CatalogItems = new Dictionary<int, ComputerCatalogItem>();
        
        // seed design-time data
        SeedDesignTimeData();
    }

    private void SeedDesignTimeData()
    {
        // Add manufacturers
        Manufacturers = new Dictionary<int, ComponentManufacturer>
        {
            { 1, new ComponentManufacturer { Name = "Intel", ComponentType = ComponentType.Cpu | ComponentType.Gpu } },
            { 2, new ComponentManufacturer { Name = "AMD", ComponentType = ComponentType.Cpu | ComponentType.Gpu } },
            { 3, new ComponentManufacturer { Name = "NVIDIA", ComponentType = ComponentType.Gpu } }
        };

        // Add catalog items
        CatalogItems = new Dictionary<int, ComputerCatalogItem>
        {
            {
                1, new ComputerCatalogItem
                {
                    Memory = new MemorySpecification { Quantity = 8, Unit = StorageUnit.GB },
                    StorageSpecification = new StorageSpecification
                        { Quantity = 1, Unit = StorageUnit.TB, StorageType = StorageType.SSD },
                    UsbSpecification = new Dictionary<UsbType, int>
                    {
                        { UsbType.USB3, 2 },
                        { UsbType.USB2, 4 }
                    },
                    GpuDescriptor = new ComponentDescriptor
                        { Manufacturer = Manufacturers[3], ModelName = "GeForce GTX 770" },
                    Weight = new Weight(8100, WeightUnit.Grams),
                    PsuRating = 500,
                    CpuDescriptor = new ComponentDescriptor
                        { Manufacturer = Manufacturers[1], ModelName = "Celeron N3050" }
                }
            },
            {
                2, new ComputerCatalogItem
                {
                    Memory = new MemorySpecification
                    {
                        Quantity = 16, Unit = StorageUnit.GB
                    },
                    StorageSpecification = new StorageSpecification
                    {
                        Quantity = 2, Unit = StorageUnit.TB, StorageType = StorageType.HDD
                    },
                    UsbSpecification = new Dictionary<UsbType, int>
                    {
                        { UsbType.USB3, 3 },
                        { UsbType.USB2, 4 }
                    },
                    GpuDescriptor = new ComponentDescriptor
                    {
                        Manufacturer = Manufacturers[3],
                        ModelName = "GeForce GTX 960"
                    },
                    Weight = new Weight(12, WeightUnit.Kilograms),
                    PsuRating = 500,
                    CpuDescriptor = new ComponentDescriptor
                    {
                        Manufacturer = Manufacturers[2],
                        ModelName = "FX 4300"
                    }
                }
            },
            {
                3, new ComputerCatalogItem
                {
                    Memory = new MemorySpecification { Quantity = 16, Unit = StorageUnit.GB },
                    StorageSpecification = new StorageSpecification
                        { Quantity = 3, Unit = StorageUnit.TB, StorageType = StorageType.HDD },
                    UsbSpecification = new Dictionary<UsbType, int>
                    {
                        { UsbType.USB3, 4 },
                        { UsbType.USB2, 4 }
                    },
                    GpuDescriptor = new ComponentDescriptor
                    {
                        Manufacturer = Manufacturers[2],
                        ModelName = "Radeon R7360"
                    },
                    Weight = new Weight(16, WeightUnit.Pounds),
                    PsuRating = 450,
                    CpuDescriptor = new ComponentDescriptor
                    {
                        Manufacturer = Manufacturers[2],
                        ModelName = "Athlon 5150 Quad-Core"
                    }
                }
            }
        };
    }

    public Dictionary<int, ComputerCatalogItem> GetAllCatalogItems()
    {
        var result = new Dictionary<int, ComputerCatalogItem>(CatalogItems);
        return result;
    }

    public Dictionary<int, ComponentManufacturer> GetAllComponentManufacturers(
        ComponentType? componentTypeFilter = null)
    {
        return componentTypeFilter is null
            ? new Dictionary<int, ComponentManufacturer>(Manufacturers)
            : Manufacturers.Where(m => (m.Value.ComponentType & componentTypeFilter.Value) > 0).ToDictionary();
    }
}