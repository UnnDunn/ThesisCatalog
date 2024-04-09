namespace ThesisCatalog.Core.Entities;

public record ComponentDescriptor
{
    public ComponentManufacturer? Manufacturer { get; set; }
    public string ModelName { get; set; } = string.Empty;

    public override string ToString() 
        => string.Concat(Manufacturer?.Name ?? string.Empty, " ", ModelName).Trim();
}