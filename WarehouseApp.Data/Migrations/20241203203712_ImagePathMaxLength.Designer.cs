﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarehouseApp.Data;

#nullable disable

namespace WarehouseApp.Data.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    [Migration("20241203203712_ImagePathMaxLength")]
    partial class ImagePathMaxLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Category Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Category Name");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Message Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Message content");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Message type");

                    b.Property<Guid?>("ReceiverId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Person (WarehouseWorker) who read message");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("User who can send message (SenderMassageUser)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2")
                        .HasComment("Sent date");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasComment("Message status");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.MessagesForWarehouseWorker", b =>
                {
                    b.Property<Guid>("WarehouseWorkerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Warehouse Worker Identifier");

                    b.Property<int>("MassageId")
                        .HasColumnType("int")
                        .HasComment("Message Identifier");

                    b.HasKey("WarehouseWorkerId", "MassageId");

                    b.HasIndex("MassageId");

                    b.ToTable("MessagesForWarehouseWorkers");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Order Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasComment("Order date");

                    b.Property<Guid>("RequestedByWorkerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Worker who requests an order");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasComment("Order status");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Supplier Identifier");

                    b.HasKey("OrderId");

                    b.HasIndex("RequestedByWorkerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasComment("Order Identifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.Property<int>("QuantityOrdered")
                        .HasColumnType("int")
                        .HasComment("Quantity of a particular product");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Product description");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Image path in file system");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Product name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Product Price");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.Property<long>("StockQuantity")
                        .HasColumnType("bigint")
                        .HasComment("Product quantity on stock");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Category Identifier");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Request Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<string>("Note")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Warehouse worker who checked the request");

                    b.Property<Guid?>("ProcessedByWorkerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Warehouse worker who checked the request");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2")
                        .HasComment("Request date");

                    b.Property<Guid>("RequesterId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("User who can request and buy product");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Request status");

                    b.HasKey("RequestId");

                    b.HasIndex("ProcessedByWorkerId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.RequestProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.Property<int>("RequestId")
                        .HasColumnType("int")
                        .HasComment("Request Identifier");

                    b.Property<decimal>("PriceUponRequest")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Product price on request");

                    b.Property<int>("QuantityRequested")
                        .HasColumnType("int")
                        .HasComment("Requested product quantity");

                    b.HasKey("ProductId", "RequestId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Sale Identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Customer Identifier");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int")
                        .HasComment("Whether the sale was made by request");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2")
                        .HasComment("Sale date");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount owed");

                    b.HasKey("SaleId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RequestId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.SaleProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Product Identifier");

                    b.Property<int>("SaleId")
                        .HasColumnType("int")
                        .HasComment("Sale Identifier");

                    b.Property<int>("QuantitySold")
                        .HasColumnType("int")
                        .HasComment("Quantity of product sold");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Product unit price");

                    b.HasKey("ProductId", "SaleId");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.SenderMessageUser", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.ApplicationUser");

                    b.ToTable("SenderMessageUsers");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.WarehouseWorker", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.ApplicationUser");

                    b.Property<DateTime?>("EndWork")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("StartWork")
                        .HasColumnType("datetime2");

                    b.ToTable("WarehouseWorkers", (string)null);
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.SenderMessageUser");

                    b.ToTable("RequesterAndBuyerUsers");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Supplier", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.SenderMessageUser");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PreferredDeliveryMethod")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("factoryLocation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable("Suppliers", (string)null);
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Customer", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Distributor", b =>
                {
                    b.HasBaseType("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser");

                    b.Property<string>("BusinessAddress")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CompanyPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("DiscountRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LicenseExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MOL")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.ToTable("Distributors", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Message", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.WarehouseWorker", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.HasOne("WarehouseApp.Data.Models.Users.SenderMessageUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.MessagesForWarehouseWorker", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Message", "Massage")
                        .WithMany("MessagesForWarehouseWorker")
                        .HasForeignKey("MassageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Users.WarehouseWorker", "WarehouseWorker")
                        .WithMany()
                        .HasForeignKey("WarehouseWorkerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Massage");

                    b.Navigation("WarehouseWorker");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Order", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.WarehouseWorker", "RequestedByWorker")
                        .WithMany()
                        .HasForeignKey("RequestedByWorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Users.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestedByWorker");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.OrderProduct", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.ProductCategory", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Request", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.WarehouseWorker", "ProcessedByWorker")
                        .WithMany()
                        .HasForeignKey("ProcessedByWorkerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ProcessedByWorker");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.RequestProduct", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Product", "Product")
                        .WithMany("RequestProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Request", "Request")
                        .WithMany("RequestProducts")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Sale", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");

                    b.Navigation("Customer");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.SaleProduct", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Product", "Product")
                        .WithMany("SaleProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WarehouseApp.Data.Models.Sale", "Sale")
                        .WithMany("SaleProducts")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.SenderMessageUser", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.SenderMessageUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.WarehouseWorker", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.ApplicationUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.WarehouseWorker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.SenderMessageUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Supplier", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.SenderMessageUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.Supplier", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Customer", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.Customer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Users.Distributor", b =>
                {
                    b.HasOne("WarehouseApp.Data.Models.Users.RequesterAndBuyerUser", null)
                        .WithOne()
                        .HasForeignKey("WarehouseApp.Data.Models.Users.Distributor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Message", b =>
                {
                    b.Navigation("MessagesForWarehouseWorker");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Product", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("ProductCategories");

                    b.Navigation("RequestProducts");

                    b.Navigation("SaleProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Request", b =>
                {
                    b.Navigation("RequestProducts");
                });

            modelBuilder.Entity("WarehouseApp.Data.Models.Sale", b =>
                {
                    b.Navigation("SaleProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
