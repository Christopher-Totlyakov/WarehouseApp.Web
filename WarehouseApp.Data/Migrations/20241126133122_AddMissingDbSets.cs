using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_RequesterAndBuyerUser_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_RequesterAndBuyerUser_Id",
                table: "Distributors");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SenderMessageUser_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_RequesterAndBuyerUser_SenderMessageUser_Id",
                table: "RequesterAndBuyerUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequesterAndBuyerUser_RequesterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RequesterAndBuyerUser_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SenderMessageUser_AspNetUsers_Id",
                table: "SenderMessageUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SenderMessageUser_Id",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderMessageUser",
                table: "SenderMessageUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequesterAndBuyerUser",
                table: "RequesterAndBuyerUser");

            migrationBuilder.RenameTable(
                name: "SenderMessageUser",
                newName: "SenderMessageUsers");

            migrationBuilder.RenameTable(
                name: "RequesterAndBuyerUser",
                newName: "RequesterAndBuyerUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderMessageUsers",
                table: "SenderMessageUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequesterAndBuyerUsers",
                table: "RequesterAndBuyerUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_RequesterAndBuyerUsers_Id",
                table: "Customers",
                column: "Id",
                principalTable: "RequesterAndBuyerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_RequesterAndBuyerUsers_Id",
                table: "Distributors",
                column: "Id",
                principalTable: "RequesterAndBuyerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SenderMessageUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "SenderMessageUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequesterAndBuyerUsers_SenderMessageUsers_Id",
                table: "RequesterAndBuyerUsers",
                column: "Id",
                principalTable: "SenderMessageUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequesterAndBuyerUsers_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "RequesterAndBuyerUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RequesterAndBuyerUsers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "RequesterAndBuyerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SenderMessageUsers_AspNetUsers_Id",
                table: "SenderMessageUsers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SenderMessageUsers_Id",
                table: "Suppliers",
                column: "Id",
                principalTable: "SenderMessageUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_RequesterAndBuyerUsers_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_RequesterAndBuyerUsers_Id",
                table: "Distributors");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SenderMessageUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_RequesterAndBuyerUsers_SenderMessageUsers_Id",
                table: "RequesterAndBuyerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequesterAndBuyerUsers_RequesterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RequesterAndBuyerUsers_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SenderMessageUsers_AspNetUsers_Id",
                table: "SenderMessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SenderMessageUsers_Id",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SenderMessageUsers",
                table: "SenderMessageUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequesterAndBuyerUsers",
                table: "RequesterAndBuyerUsers");

            migrationBuilder.RenameTable(
                name: "SenderMessageUsers",
                newName: "SenderMessageUser");

            migrationBuilder.RenameTable(
                name: "RequesterAndBuyerUsers",
                newName: "RequesterAndBuyerUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SenderMessageUser",
                table: "SenderMessageUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequesterAndBuyerUser",
                table: "RequesterAndBuyerUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_RequesterAndBuyerUser_Id",
                table: "Customers",
                column: "Id",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_RequesterAndBuyerUser_Id",
                table: "Distributors",
                column: "Id",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SenderMessageUser_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "SenderMessageUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequesterAndBuyerUser_SenderMessageUser_Id",
                table: "RequesterAndBuyerUser",
                column: "Id",
                principalTable: "SenderMessageUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequesterAndBuyerUser_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RequesterAndBuyerUser_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SenderMessageUser_AspNetUsers_Id",
                table: "SenderMessageUser",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SenderMessageUser_Id",
                table: "Suppliers",
                column: "Id",
                principalTable: "SenderMessageUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
