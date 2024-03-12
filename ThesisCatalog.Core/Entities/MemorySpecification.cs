namespace ThesisCatalog.Core.Entities;

public record MemorySpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; } = StorageUnit.GB;
}