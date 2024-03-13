using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ComponentType ComponentTypes { get; set; }
}