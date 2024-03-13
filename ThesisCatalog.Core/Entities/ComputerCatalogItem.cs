namespace ThesisCatalog.Core.Entities;

public record ComputerCatalogItem
{
    public int Id { get; set; }
    public MemorySpecification Memory { get; set; } = null!;
    public StorageSpecification StorageSpecification { get; set; } = null!;
    public UsbSpecification UsbSpecification { get; set; } = null!;
    public Weight Weight { get; set; } = null!;
    public int PsuRating { get; set; }
    public ComponentDescriptor GpuDescriptor { get; set; } = null!;
    public ComponentDescriptor CpuDescriptor { get; set; } = null!;
}