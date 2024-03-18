using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ComponentType ComponentTypes { get; set; }

    public static explicit operator ComponentManufacturer(Manufacturer m) =>
        new()
        {
            Id = m.Id, Name = m.Name, ComponentType = m.ComponentTypes
        };
    
    public static explicit operator Manufacturer(ComponentManufacturer cm) =>
        new() { Id = cm.Id, Name = cm.Name, ComponentTypes = cm.ComponentType };
}