using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record MemorySpecification
{
    public long MemoryBytes { get; set; }
    public StorageUnit MemoryDisplayUnit { get; set; }
}