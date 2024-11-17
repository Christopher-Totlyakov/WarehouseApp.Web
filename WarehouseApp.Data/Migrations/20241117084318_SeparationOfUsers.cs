using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeparationOfUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Messages_ReceiverId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orders_SupplierId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagesForWarehouseWorkers_AspNetUsers_WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_RequestedByWorkerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ProcessedByWorkerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_AspNetUsers_SupplierId",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReceiverId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SupplierId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "SenderMessageUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderMessageUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenderMessageUser_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    factoryLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredDeliveryMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suppliers_Orders_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndWork = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseWorkers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseWorkers_Messages_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Messages",
                        principalColumn: "MessageId");
                });

            migrationBuilder.CreateTable(
                name: "RequesterAndBuyerUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequesterAndBuyerUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequesterAndBuyerUser_SenderMessageUser_Id",
                        column: x => x.Id,
                        principalTable: "SenderMessageUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_RequesterAndBuyerUser_Id",
                        column: x => x.Id,
                        principalTable: "RequesterAndBuyerUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MOL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributors_RequesterAndBuyerUser_Id",
                        column: x => x.Id,
                        principalTable: "RequesterAndBuyerUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierId",
                table: "Suppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseWorkers_ReceiverId",
                table: "WarehouseWorkers",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SenderMessageUser_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "SenderMessageUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesForWarehouseWorkers_WarehouseWorkers_WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers",
                column: "WarehouseWorkerId",
                principalTable: "WarehouseWorkers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WarehouseWorkers_RequestedByWorkerId",
                table: "Orders",
                column: "RequestedByWorkerId",
                principalTable: "WarehouseWorkers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequesterAndBuyerUser_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_WarehouseWorkers_ProcessedByWorkerId",
                table: "Requests",
                column: "ProcessedByWorkerId",
                principalTable: "WarehouseWorkers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RequesterAndBuyerUser_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "RequesterAndBuyerUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                table: "SupplierOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SenderMessageUser_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagesForWarehouseWorkers_WarehouseWorkers_WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WarehouseWorkers_RequestedByWorkerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequesterAndBuyerUser_RequesterId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_WarehouseWorkers_ProcessedByWorkerId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RequesterAndBuyerUser_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Suppliers_SupplierId",
                table: "SupplierOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "WarehouseWorkers");

            migrationBuilder.DropTable(
                name: "RequesterAndBuyerUser");

            migrationBuilder.DropTable(
                name: "SenderMessageUser");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReceiverId",
                table: "AspNetUsers",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SupplierId",
                table: "AspNetUsers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Messages_ReceiverId",
                table: "AspNetUsers",
                column: "ReceiverId",
                principalTable: "Messages",
                principalColumn: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orders_SupplierId",
                table: "AspNetUsers",
                column: "SupplierId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesForWarehouseWorkers_AspNetUsers_WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers",
                column: "WarehouseWorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_RequestedByWorkerId",
                table: "Orders",
                column: "RequestedByWorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ProcessedByWorkerId",
                table: "Requests",
                column: "ProcessedByWorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_AspNetUsers_SupplierId",
                table: "SupplierOrders",
                column: "SupplierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
