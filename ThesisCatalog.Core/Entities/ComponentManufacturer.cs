namespace ThesisCatalog.Core.Entities;

public record ComponentManufacturer
{
    public string Name { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = ComponentType.None;
}