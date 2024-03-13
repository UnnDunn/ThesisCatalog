using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisCatalog.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnsignedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Manufacturers_CpuDescriptor_ManufacturerId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Manufacturers_GpuDescriptor_ManufacturerId",
                table: "CatalogItems");

            migrationBuilder.AlterColumn<long>(
                name: "Weight_WeightGrams",
                table: "CatalogItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "StorageSpecification_StorageBytes",
                table: "CatalogItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<short>(
                name: "PsuRating",
                table: "CatalogItems",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "MemorySpecification_MemoryBytes",
                table: "CatalogItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<string>(
                name: "GpuDescriptor_ModelName",
                table: "CatalogItems",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CpuDescriptor_ModelName",
                table: "CatalogItems",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Manufacturers_CpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "CpuDescriptor_ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Manufacturers_GpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "GpuDescriptor_ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Manufacturers_CpuDescriptor_ManufacturerId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Manufacturers_GpuDescriptor_ManufacturerId",
                table: "CatalogItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight_WeightGrams",
                table: "CatalogItems",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "StorageSpecification_StorageBytes",
                table: "CatalogItems",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PsuRating",
                table: "CatalogItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<decimal>(
                name: "MemorySpecification_MemoryBytes",
                table: "CatalogItems",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "GpuDescriptor_ModelName",
                table: "CatalogItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "CpuDescriptor_ModelName",
                table: "CatalogItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Manufacturers_CpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "CpuDescriptor_ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Manufacturers_GpuDescriptor_ManufacturerId",
                table: "CatalogItems",
                column: "GpuDescriptor_ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
