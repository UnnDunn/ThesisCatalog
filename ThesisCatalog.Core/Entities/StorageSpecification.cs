namespace ThesisCatalog.Core.Entities;

public record StorageSpecification
{
    public int Quantity { get; set; }
    public StorageUnit Unit { get; set; }
    public StorageType StorageType { get; set; }

    public override string ToString()
    {
        var storageType = StorageType switch
        {
            StorageType.HDD => "HDD",
            StorageType.SSD => "SSD",
            _ => string.Empty
        };
        
        var suffix = Unit switch
        {
            StorageUnit.GB => "GB",
            StorageUnit.TB => "TB",
            StorageUnit.MB => "MB",
            _ => string.Empty
        };
        return string.Concat(Quantity, suffix, " ", storageType);
    }
}
