using System.Text.Json.Serialization;

namespace ThesisCatalog.Core.Entities;

public record MemorySpecification
{
    public long ByteQuantity { get; init; }
    public StorageUnit DisplayUnit { get; init; } = StorageUnit.GB;

    [JsonIgnore]
    public int DisplayQuantity =>
        Convert.ToInt32(Math.Round(ByteQuantity / Math.Pow(1024, (int)DisplayUnit)));
    
    public MemorySpecification(int quantity, StorageUnit displayUnit)
    {
        var multiplier = Math.Pow(1024, (int)displayUnit);
        var byteQuantity = quantity * multiplier;

        ByteQuantity = Convert.ToInt64(Math.Round(byteQuantity));
        DisplayUnit = displayUnit;
    }

    [JsonConstructor]
    public MemorySpecification(long byteQuantity, StorageUnit displayUnit)
    {
        ByteQuantity = byteQuantity;
        DisplayUnit = displayUnit;
    }
    
    public MemorySpecification() {}
    
    public override string ToString()
    {
        var divisor = Math.Pow(1024, (int)DisplayUnit);
        var quantity = Convert.ToInt32(Math.Round(ByteQuantity / divisor));
        
        var suffix = DisplayUnit switch
        {
            StorageUnit.kB => "kB",
            StorageUnit.GB => "GB",
            StorageUnit.TB => "TB",
            StorageUnit.MB => "MB",
            _ => string.Empty
        };
        return $"{quantity} {suffix}";
    }
}