namespace ThesisCatalog.Core.Entities;

public record ComponentDescriptor
{
    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public string ModelName { get; set; } = null!;
}