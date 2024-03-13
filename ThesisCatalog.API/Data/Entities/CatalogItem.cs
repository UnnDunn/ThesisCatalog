using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisCatalog.API.Data.Entities;

public record CatalogItem
{
    public int Id { get; set; }
    public MemorySpecification MemorySpecification { get; set; } = null!;
    public StorageSpecification StorageSpecification { get; set; } = null!;
    public short PsuRating { get; set; }
    public WeightSpecification Weight { get; set; } = null!;

    public ComponentDescriptor CpuDescriptor { get; set; } = null!;
    public ComponentDescriptor GpuDescriptor { get; set; } = null!;
    
    [NotMapped]
    public  ICollection<UsbSpecification> UsbSpecifications { get; set; } = null!;
}