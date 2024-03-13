using Microsoft.EntityFrameworkCore;
using ThesisCatalog.API.Data.Entities;

namespace ThesisCatalog.API.Data;

public class ThesisCatalogDbContext : DbContext
{
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<CatalogItem> CatalogItems { get; set; } = null!;

    public ThesisCatalogDbContext(DbContextOptions<ThesisCatalogDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CatalogItem>()
            .OwnsOne(c => c.MemorySpecification);
        modelBuilder.Entity<CatalogItem>()
            .OwnsOne(c => c.StorageSpecification);
        modelBuilder.Entity<CatalogItem>().OwnsOne(c => c.Weight);
        modelBuilder.Entity<CatalogItem>()
            .OwnsOne(c => c.CpuDescriptor, od =>
            {
                od.HasOne(cd => cd.Manufacturer)
                    .WithMany()
                    .HasForeignKey(cd => cd.ManufacturerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        modelBuilder.Entity<CatalogItem>()
            .OwnsOne(c => c.GpuDescriptor, od =>
            {
                od.HasOne(cd => cd.Manufacturer)
                    .WithMany()
                    .HasForeignKey(cd => cd.ManufacturerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        modelBuilder.Entity<CatalogItem>()
            .OwnsMany(c => c.UsbSpecifications, c =>
            {
                c.WithOwner().HasForeignKey(usb => usb.CatalogItemId);
                c.HasKey(u => new { u.CatalogItemId, u.UsbType });
            });
    }
}