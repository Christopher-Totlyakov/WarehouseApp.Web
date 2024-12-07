using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductNameMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "Product name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Product name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Product name",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "Product name");
        }
    }
}
