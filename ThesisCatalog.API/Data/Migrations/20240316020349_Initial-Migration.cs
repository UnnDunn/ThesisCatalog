using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisCatalog.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentTypes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemorySpecification_MemoryBytes = table.Column<long>(type: "bigint", nullable: false),
                    MemorySpecification_MemoryDisplayUnit = table.Column<int>(type: "int", nullable: false),
                    StorageSpecification_StorageBytes = table.Column<long>(type: "bigint", nullable: false),
                    StorageSpecification_StorageDisplayUnit = table.Column<int>(type: "int", nullable: false),
                    StorageSpecification_StorageType = table.Column<int>(type: "int", nullable: false),
                    PsuRating = table.Column<short>(type: "smallint", nullable: false),
                    Weight_WeightGrams = table.Column<long>(type: "bigint", nullable: false),
                    Weight_WeightDisplayUnit = table.Column<int>(type: "int", nullable: false),
                    CpuDescriptor_ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    CpuDescriptor_ModelName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GpuDescriptor_ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    GpuDescriptor_ModelName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Manufacturers_CpuDescriptor_ManufacturerId",
                        column: x => x.CpuDescriptor_ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CatalogItems_Manufacturers_GpuDescriptor_ManufacturerId",
                        column: x => x.GpuDescriptor_ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsbSpecification",
                columns: table => new
                {
                    CatalogItemId = table.Column<int>(type: "int", nullable: false),
                    UsbType = table.Column<int>(type: "int", nullable: false),
                    PortCount = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsbSpecification", x => new { x.CatalogItemId, x.UsbType });
                    table.ForeignKey(
                        name: "FK_UsbSpecification_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "CpuDescriptor_ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_GpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "GpuDescriptor_ManufacturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsbSpecification");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
