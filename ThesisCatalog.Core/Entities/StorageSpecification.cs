using System.Text.Json.Serialization;

namespace ThesisCatalog.Core.Entities;

public record StorageSpecification
{
    public long ByteQuantity { get; init; }
    public StorageUnit DisplayUnit { get; init; }
    public StorageType StorageType { get; init; }

    [JsonIgnore]
    public int DisplayQuantity =>
        (int)Math.Round(ByteQuantity / Math.Pow(1024, (int)DisplayUnit));

    public StorageSpecification(int quantity, StorageUnit displayUnit, StorageType type)
    {
        var multiplier = Math.Pow(1024, (int)displayUnit);
        var byteQuantity = quantity * multiplier;

        ByteQuantity = Convert.ToInt64(Math.Round(byteQuantity));
        DisplayUnit = displayUnit;
        StorageType = type;
    }

    [JsonConstructor]
    public StorageSpecification(long byteQuantity, StorageUnit displayUnit, StorageType storageType)
    {
        ByteQuantity = byteQuantity;
        DisplayUnit = displayUnit;
        StorageType = storageType;
    }
    
    public StorageSpecification() {}

    public override string ToString()
    {
        var divisor = Math.Pow(1024, (int)DisplayUnit);
        var quantity = Convert.ToInt32(Math.Round(ByteQuantity / divisor));
        
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
        return $"{quantity}{suffix} {storageType}";
    }
}
