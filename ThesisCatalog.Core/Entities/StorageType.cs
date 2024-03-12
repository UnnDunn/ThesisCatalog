namespace ThesisCatalog.Core.Entities;

public enum StorageType
{
    HDD, SSD
}

public enum StorageUnit
{
    MB, GB, TB
}

public enum UsbType
{
    USB2,
    USB3,
    USBC
}

public enum WeightUnit
{
    kg, lb
}

[Flags]
public enum ComponentType
{
    None = 0,
    Cpu = 1,
    Gpu = 2,
    Memory = 4,
    Storage = 8,
    Psu = 16
}

public record ComponentManufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = ComponentType.None;
}

public record ComponentDescriptor
{
    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public string ModelName { get; set; } = null!;
}

public record MemorySpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; } = StorageUnit.GB;
}

public record StorageSpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; }
    public StorageType StorageType { get; set; }
}

public record Weight
{
    public int WeightAmount { get; set; }
    public WeightUnit Unit { get; set; }
}

public record ComputerCatalogItem
{
    public MemorySpecification Memory { get; set; } = null!;
    public StorageSpecification StorageSpecification { get; set; } = null!;
    public Dictionary<UsbType, int> UsbSpecification { get; set; } = new();
    public Weight Weight { get; set; } = null!;
    public int PsuRating { get; set; }
    public ComponentDescriptor GpuDescriptor { get; set; } = null!;
    public ComponentDescriptor CpuDescriptor { get; set; } = null!;
}
