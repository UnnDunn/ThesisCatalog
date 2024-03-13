namespace ThesisCatalog.Core.Entities;

public record ComponentManufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = ComponentType.None;
}