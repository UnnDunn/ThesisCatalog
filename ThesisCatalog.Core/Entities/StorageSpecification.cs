namespace ThesisCatalog.Core.Entities;

public record StorageSpecification
{
    public ulong ByteQuantity { get; init; }
    public StorageUnit DisplayUnit { get; init; }
    public StorageType StorageType { get; init; }

    public StorageSpecification(uint quantity, StorageUnit displayUnit, StorageType type)
    {
        var multiplier = Math.Pow(1024, (int)displayUnit);
        var byteQuantity = quantity * multiplier;

        ByteQuantity = Convert.ToUInt64(Math.Round(byteQuantity));
        DisplayUnit = displayUnit;
        StorageType = type;
    }

    public override string ToString()
    {
        var divisor = Math.Pow(1024, (int)DisplayUnit);
        var quantity = ByteQuantity / divisor;
        
        var storageType = StorageType switch
        {
            StorageType.HDD => "HDD",
            StorageType.SSD => "SSD",
            _ => string.Empty
        };
        
        var suffix = DisplayUnit switch
        {
            StorageUnit.kB => "kB",
            StorageUnit.GB => "GB",
            StorageUnit.TB => "TB",
            StorageUnit.MB => "MB",
            _ => string.Empty
        };
        return $"{quantity:N}{suffix} {storageType}";
    }
}
