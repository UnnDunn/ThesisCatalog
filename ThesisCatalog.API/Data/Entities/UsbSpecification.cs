using Microsoft.EntityFrameworkCore;
using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record UsbSpecification
{
    public int CatalogItemId { get; set; }
    public UsbType UsbType { get; set; }
    public ushort PortCount { get; set; }
}