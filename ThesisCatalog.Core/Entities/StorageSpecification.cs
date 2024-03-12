namespace ThesisCatalog.Core.Entities;

public record StorageSpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; }
    public StorageType StorageType { get; set; }
}