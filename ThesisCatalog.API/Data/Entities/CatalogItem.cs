using System.ComponentModel.DataAnnotations.Schema;
using ThesisCatalog.Core.Entities;

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
    public ICollection<UsbSpecification> UsbSpecifications { get; set; } = null!;

    public string SearchText { get; set; } = string.Empty;

    public static explicit operator ComputerCatalogItem(CatalogItem item) =>
        new()
        {
            Id = item.Id,
            Memory =
                new Core.Entities.MemorySpecification
                {
                    ByteQuantity = item.MemorySpecification.MemoryBytes,
                    DisplayUnit = item.MemorySpecification.MemoryDisplayUnit
                },
            StorageSpecification =
                new Core.Entities.StorageSpecification
                {
                    ByteQuantity = item.StorageSpecification.StorageBytes,
                    DisplayUnit = item.StorageSpecification.StorageDisplayUnit,
                    StorageType = item.StorageSpecification.StorageType
                },
            UsbSpecification =
                new Core.Entities.UsbSpecification(item.UsbSpecifications.ToDictionary(u => u.UsbType, u => u.PortCount)),
            Weight = new Weight(item.Weight.WeightGrams, item.Weight.WeightDisplayUnit),
            PsuRating = item.PsuRating,
            GpuDescriptor =
                new Core.Entities.ComponentDescriptor
                {
                    Manufacturer =
                        new ComponentManufacturer
                        {
                            Id = item.GpuDescriptor.Manufacturer.Id,
                            Name = item.GpuDescriptor.Manufacturer.Name,
                            ComponentType = item.GpuDescriptor.Manufacturer.ComponentTypes
                        },
                    ModelName = item.GpuDescriptor.ModelName
                },
            CpuDescriptor = new Core.Entities.ComponentDescriptor
            {
                Manufacturer =
                    new ComponentManufacturer
                    {
                        Id = item.CpuDescriptor.Manufacturer.Id,
                        Name = item.CpuDescriptor.Manufacturer.Name,
                        ComponentType = item.CpuDescriptor.Manufacturer.ComponentTypes
                    },
                ModelName = item.CpuDescriptor.ModelName
            }
        };

    public static explicit operator CatalogItem(ComputerCatalogItem item) =>
        new()
        {
            MemorySpecification =
                new MemorySpecification
                {
                    MemoryBytes = item.Memory.ByteQuantity, MemoryDisplayUnit = item.Memory.DisplayUnit
                },
            StorageSpecification =
                new StorageSpecification
                {
                    StorageBytes = item.StorageSpecification.ByteQuantity,
                    StorageDisplayUnit = item.StorageSpecification.DisplayUnit,
                    StorageType = item.StorageSpecification.StorageType
                },
            Weight =
                new WeightSpecification
                {
                    WeightGrams = item.Weight.Grams, WeightDisplayUnit = item.Weight.DisplayUnit
                },
            PsuRating = item.PsuRating,
            CpuDescriptor =
                new ComponentDescriptor
                {
                    ModelName = item.CpuDescriptor.ModelName,
                    ManufacturerId = item.CpuDescriptor.Manufacturer?.Id ?? 0
                },
            GpuDescriptor =
                new ComponentDescriptor
                {
                    ModelName = item.GpuDescriptor.ModelName,
                    ManufacturerId = item.GpuDescriptor.Manufacturer?.Id ?? 0
                },
            UsbSpecifications =
                item.UsbSpecification.UsbPorts
                    .Select(u => new UsbSpecification { UsbType = u.Key, PortCount = u.Value })
                    .ToList()
        };
}