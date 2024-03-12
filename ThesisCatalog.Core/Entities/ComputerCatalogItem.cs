namespace ThesisCatalog.Core.Entities;

public record ComputerCatalogItem
{
    public MemorySpecification Memory { get; set; } = null!;
    public StorageSpecification StorageSpecification { get; set; } = null!;
    public Dictionary<UsbType, int> UsbSpecification { get; set; } = new();
    public Weight Weight { get; set; } = null!;
    public int PsuRating { get; set; }
    public ComponentDescriptor GpuDescriptor { get; set; } = null!;
    public ComponentDescriptor CpuDescriptor { get; set; } = null!;
}