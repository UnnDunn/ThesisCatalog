namespace ThesisCatalog.Core.Entities;

public record ComponentDescriptor
{
    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public string ModelName { get; set; } = null!;

    public override string ToString() 
        => string.Concat(Manufacturer.Name, " ", ModelName);
}