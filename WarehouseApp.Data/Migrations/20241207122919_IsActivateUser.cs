using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsActivateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivate",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivate",
                table: "AspNetUsers");
        }
    }
}
