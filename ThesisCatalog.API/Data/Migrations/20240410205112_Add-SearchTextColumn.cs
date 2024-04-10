using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisCatalog.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchTextColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchText",
                table: "CatalogItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchText",
                table: "CatalogItems");
        }
    }
}
