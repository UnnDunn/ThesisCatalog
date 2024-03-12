namespace ThesisCatalog.Core.Entities;

public record MemorySpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; } = StorageUnit.GB;

    public override string ToString()
    {
        var suffix = Unit switch
        {
            StorageUnit.GB => "GB",
            StorageUnit.TB => "TB",
            StorageUnit.MB => "MB",
            _ => string.Empty
        };
        return string.Concat(Quantity, suffix);
    }
}