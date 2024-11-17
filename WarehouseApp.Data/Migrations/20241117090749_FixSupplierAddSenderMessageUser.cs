using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDistributor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_Id",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SenderMessageUser_Id",
                table: "Suppliers",
                column: "Id",
                principalTable: "SenderMessageUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SenderMessageUser_Id",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_Id",
                table: "Suppliers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
