using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record StorageSpecification
{
    public long StorageBytes { get; set; }
    public StorageUnit StorageDisplayUnit { get; set; }
    public StorageType StorageType { get; set; }
}