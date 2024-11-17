using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixConnectionsAddMissingAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_ProductCategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseWorkers_Messages_ReceiverId",
                table: "WarehouseWorkers");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseWorkers_ReceiverId",
                table: "WarehouseWorkers");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "WarehouseWorkers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoriesId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductCategoriesId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "WarehouseWorkers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "WarehouseWorkers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "factoryLocation",
                table: "Suppliers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PreferredDeliveryMethod",
                table: "Suppliers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Suppliers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Total amount owed",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                comment: "Sale date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Sales",
                type: "int",
                nullable: true,
                comment: "Whether the sale was made by request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Customer Identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "Sales",
                type: "int",
                nullable: false,
                comment: "Sale Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Product unit price",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "QuantitySold",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                comment: "Quantity of product sold",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                comment: "Sale Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                comment: "Product Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "Request status",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequesterId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                comment: "User who can request and buy product",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                comment: "Request date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcessedByWorkerId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Warehouse worker who checked the request",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Requests",
                type: "int",
                nullable: false,
                comment: "Request Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Requests",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "Warehouse worker who checked the request");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityRequested",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                comment: "Requested product quantity",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceUponRequest",
                table: "RequestProducts",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Product price on request",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                comment: "Request Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                comment: "Product Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "StockQuantity",
                table: "Products",
                type: "bigint",
                nullable: false,
                comment: "Product quantity on stock",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Product Price",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Product name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Product description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "Product Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "Image path in file system");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                comment: "Category Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                comment: "Product Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Supplier Identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Order status",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestedByWorkerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Worker who requests an order",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                comment: "Order date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Order Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOrdered",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                comment: "Quantity of a particular product",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                comment: "Product Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                comment: "Order Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MassageId",
                table: "MessagesForWarehouseWorkers",
                type: "int",
                nullable: false,
                comment: "Message Identifier",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Warehouse Worker Identifier",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Messages",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Message status",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                comment: "Sent date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                comment: "User who can send message (SenderMassageUser)",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Person (WarehouseWorker) who read message",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "Messages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Message type",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MessageContent",
                table: "Messages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Message content",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Messages",
                type: "int",
                nullable: false,
                comment: "Message Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Distributors",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MOL",
                table: "Distributors",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyPhoneNumber",
                table: "Distributors",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Distributors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "Distributors",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessAddress",
                table: "Distributors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Category Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Category Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_WarehouseWorkers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "WarehouseWorkers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_WarehouseWorkers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "WarehouseWorkers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "WarehouseWorkers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "WarehouseWorkers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "factoryLocation",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PreferredDeliveryMethod",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Total amount owed");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Sale date");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Sales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Whether the sale was made by request");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Customer Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "Sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Sale Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Product unit price");

            migrationBuilder.AlterColumn<int>(
                name: "QuantitySold",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Quantity of product sold");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Sale Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SaleProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product Identifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComment: "Request status");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequesterId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "User who can request and buy product");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Request date");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProcessedByWorkerId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Warehouse worker who checked the request");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Request Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityRequested",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Requested product quantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceUponRequest",
                table: "RequestProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Product price on request");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Request Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "RequestProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product Identifier");

            migrationBuilder.AlterColumn<long>(
                name: "StockQuantity",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Product quantity on stock");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Product Price");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Product name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Product description");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Category Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product Identifier");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoriesId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Supplier Identifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Order status");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestedByWorkerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Worker who requests an order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Order date");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Order Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOrdered",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Quantity of a particular product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Order Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "MassageId",
                table: "MessagesForWarehouseWorkers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Message Identifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "WarehouseWorkerId",
                table: "MessagesForWarehouseWorkers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Warehouse Worker Identifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Message status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Sent date");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "User who can send message (SenderMassageUser)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Person (WarehouseWorker) who read message");

            migrationBuilder.AlterColumn<string>(
                name: "MessageType",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Message type");

            migrationBuilder.AlterColumn<string>(
                name: "MessageContent",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Message content");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Message Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MOL",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyPhoneNumber",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessAddress",
                table: "Distributors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Category Name");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Category Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoriesId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseWorkers_ReceiverId",
                table: "WarehouseWorkers",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoriesId",
                table: "ProductCategories",
                column: "ProductCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductCategoryId",
                table: "ProductCategories",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_ProductCategoriesId",
                table: "ProductCategories",
                column: "ProductCategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductCategoryId",
                table: "ProductCategories",
                column: "ProductCategoryId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseWorkers_Messages_ReceiverId",
                table: "WarehouseWorkers",
                column: "ReceiverId",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }
    }
}
