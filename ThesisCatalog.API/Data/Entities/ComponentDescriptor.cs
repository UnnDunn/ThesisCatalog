using System.ComponentModel.DataAnnotations;

namespace ThesisCatalog.API.Data.Entities;

public record ComponentDescriptor
{
    public int ManufacturerId { get; set; }
    [MaxLength(250)]
    public string ModelName { get; set; } = null!;

    public Manufacturer Manufacturer { get; set; } = null!;
}