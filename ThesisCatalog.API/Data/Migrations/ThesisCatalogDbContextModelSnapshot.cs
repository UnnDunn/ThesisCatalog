﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisCatalog.API.Data;

#nullable disable

namespace ThesisCatalog.API.Data.Migrations
{
    [DbContext(typeof(ThesisCatalogDbContext))]
    partial class ThesisCatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThesisCatalog.API.Data.Entities.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("PsuRating")
                        .HasColumnType("smallint");

                    b.Property<string>("SearchText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatalogItems");
                });

            modelBuilder.Entity("ThesisCatalog.API.Data.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentTypes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("ThesisCatalog.API.Data.Entities.CatalogItem", b =>
                {
                    b.OwnsOne("ThesisCatalog.API.Data.Entities.ComponentDescriptor", "CpuDescriptor", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<int>("ManufacturerId")
                                .HasColumnType("int");

                            b1.Property<string>("ModelName")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)");

                            b1.HasKey("CatalogItemId");

                            b1.HasIndex("ManufacturerId");

                            b1.ToTable("CatalogItems");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");

                            b1.HasOne("ThesisCatalog.API.Data.Entities.Manufacturer", "Manufacturer")
                                .WithMany()
                                .HasForeignKey("ManufacturerId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.Navigation("Manufacturer");
                        });

                    b.OwnsOne("ThesisCatalog.API.Data.Entities.ComponentDescriptor", "GpuDescriptor", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<int>("ManufacturerId")
                                .HasColumnType("int");

                            b1.Property<string>("ModelName")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)");

                            b1.HasKey("CatalogItemId");

                            b1.HasIndex("ManufacturerId");

                            b1.ToTable("CatalogItems");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");

                            b1.HasOne("ThesisCatalog.API.Data.Entities.Manufacturer", "Manufacturer")
                                .WithMany()
                                .HasForeignKey("ManufacturerId")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.Navigation("Manufacturer");
                        });

                    b.OwnsOne("ThesisCatalog.API.Data.Entities.MemorySpecification", "MemorySpecification", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<long>("MemoryBytes")
                                .HasColumnType("bigint");

                            b1.Property<int>("MemoryDisplayUnit")
                                .HasColumnType("int");

                            b1.HasKey("CatalogItemId");

                            b1.ToTable("CatalogItems");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");
                        });

                    b.OwnsOne("ThesisCatalog.API.Data.Entities.StorageSpecification", "StorageSpecification", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<long>("StorageBytes")
                                .HasColumnType("bigint");

                            b1.Property<int>("StorageDisplayUnit")
                                .HasColumnType("int");

                            b1.Property<int>("StorageType")
                                .HasColumnType("int");

                            b1.HasKey("CatalogItemId");

                            b1.ToTable("CatalogItems");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");
                        });

                    b.OwnsMany("ThesisCatalog.API.Data.Entities.UsbSpecification", "UsbSpecifications", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<int>("UsbType")
                                .HasColumnType("int");

                            b1.Property<short>("PortCount")
                                .HasColumnType("smallint");

                            b1.HasKey("CatalogItemId", "UsbType");

                            b1.ToTable("UsbSpecification");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");
                        });

                    b.OwnsOne("ThesisCatalog.API.Data.Entities.WeightSpecification", "Weight", b1 =>
                        {
                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("int");

                            b1.Property<int>("WeightDisplayUnit")
                                .HasColumnType("int");

                            b1.Property<long>("WeightGrams")
                                .HasColumnType("bigint");

                            b1.HasKey("CatalogItemId");

                            b1.ToTable("CatalogItems");

                            b1.WithOwner()
                                .HasForeignKey("CatalogItemId");
                        });

                    b.Navigation("CpuDescriptor")
                        .IsRequired();

                    b.Navigation("GpuDescriptor")
                        .IsRequired();

                    b.Navigation("MemorySpecification")
                        .IsRequired();

                    b.Navigation("StorageSpecification")
                        .IsRequired();

                    b.Navigation("UsbSpecifications");

                    b.Navigation("Weight")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
