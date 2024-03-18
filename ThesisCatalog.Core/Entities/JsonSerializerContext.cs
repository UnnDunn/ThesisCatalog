using System.Text.Json.Serialization;

namespace ThesisCatalog.Core.Entities;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(ComponentDescriptor))]
[JsonSerializable(typeof(ComponentManufacturer))]
[JsonSerializable(typeof(List<ComponentManufacturer>))]
[JsonSerializable(typeof(ComputerCatalogItem))]
[JsonSerializable(typeof(List<ComputerCatalogItem>))]
[JsonSerializable(typeof(MemorySpecification))]
[JsonSerializable(typeof(StorageSpecification))]
[JsonSerializable(typeof(Weight))]
[JsonSerializable(typeof(UsbSpecification))]
[JsonSerializable(typeof(List<UsbSpecification>))]
[JsonSerializable(typeof(Dictionary<UsbType, int>))]
public partial class ThesisCatalogJsonSerializerContext : JsonSerializerContext
{
}