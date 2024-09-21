﻿// <auto-generated />
using System;
using Data_Access_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(ShippingDataBase))]
    [Migration("20240920160239_AKRV1")]
    partial class AKRV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data_Access_Layer.Entity.Agent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThePrecentageOfCompanyFromOffer")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfOfferID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("GovernID");

                    b.HasIndex("TypeOfOfferID");

                    b.ToTable("agents");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("FiledJobID")
                        .HasColumnType("int");

                    b.Property<string>("Govern")
                        .HasColumnType("nvarchar(max)");

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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.HasKey("Id");

                    b.HasIndex("BranchID");

                    b.HasIndex("FiledJobID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("ApplicationUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Branch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DataAdding")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 9, 20, 19, 2, 34, 60, DateTimeKind.Local).AddTicks(9885));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("branches");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("GovernID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NormalCharge")
                        .HasColumnType("int");

                    b.Property<int>("PickUpCharge")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GovernID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.FieldJob", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateAdding")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(2853));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("fieldJobs");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.FieldPrivilege", b =>
                {
                    b.Property<int>("PrivilegeID")
                        .HasColumnType("int");

                    b.Property<int>("FieldJobID")
                        .HasColumnType("int");

                    b.Property<bool>("Add")
                        .HasColumnType("bit");

                    b.Property<bool>("Delete")
                        .HasColumnType("bit");

                    b.Property<bool>("Display")
                        .HasColumnType("bit");

                    b.Property<bool>("Edit")
                        .HasColumnType("bit");

                    b.HasKey("PrivilegeID", "FieldJobID");

                    b.HasIndex("FieldJobID");

                    b.ToTable("fieldPrivileges");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Govern", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("governs");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNumber2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdding")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(6911));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernID")
                        .HasColumnType("int");

                    b.Property<bool>("IsForVillage")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderStatusID")
                        .HasColumnType("int");

                    b.Property<string>("SellerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TypeOfChargeID")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfPaymentID")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfReceiptID")
                        .HasColumnType("int");

                    b.Property<string>("VillageOrStreet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("CityID");

                    b.HasIndex("GovernID");

                    b.HasIndex("OrderStatusID");

                    b.HasIndex("SellerID");

                    b.HasIndex("TypeOfChargeID");

                    b.HasIndex("TypeOfPaymentID");

                    b.HasIndex("TypeOfReceiptID");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.OrderStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("productStatuses");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Privilege", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("privileges");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.SpecialCharge", b =>
                {
                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("SellerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SpecialChargeForSeller")
                        .HasColumnType("int");

                    b.HasKey("CityID", "SellerID");

                    b.HasIndex("SellerID");

                    b.ToTable("specialCharges");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfCharge", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("typeOfCharges");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfOffer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("typeOfOffers");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfPayment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("typeOfPayments");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfReceipt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("typeOfReceipts");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Weight", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AdditionalWeight")
                        .HasColumnType("int");

                    b.Property<int>("DefaultWeight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("weights");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Seller", b =>
                {
                    b.HasBaseType("Data_Access_Layer.Entity.ApplicationUser");

                    b.Property<int>("PickUp")
                        .HasColumnType("int");

                    b.Property<int>("StoreCityId")
                        .HasColumnType("int");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValueOfRejectedOrder")
                        .HasColumnType("int");

                    b.HasIndex("StoreCityId");

                    b.HasDiscriminator().HasValue("Seller");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Agent", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.Branch", "Branch")
                        .WithMany("Agents")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.Govern", "Govern")
                        .WithMany("Agents")
                        .HasForeignKey("GovernID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.TypeOfOffer", "TypeOfOffer")
                        .WithMany("Agents")
                        .HasForeignKey("TypeOfOfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Govern");

                    b.Navigation("TypeOfOffer");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.ApplicationUser", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.Branch", "Branch")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.FieldJob", "FieldJob")
                        .WithMany("Users")
                        .HasForeignKey("FiledJobID");

                    b.Navigation("Branch");

                    b.Navigation("FieldJob");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.City", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.Govern", "Govern")
                        .WithMany("Cities")
                        .HasForeignKey("GovernID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Govern");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.FieldPrivilege", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.FieldJob", "FieldJob")
                        .WithMany("FieldPrivilege")
                        .HasForeignKey("FieldJobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.Privilege", "Privilege")
                        .WithMany("FieldPrivilege")
                        .HasForeignKey("PrivilegeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldJob");

                    b.Navigation("Privilege");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Order", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.Branch", "Branch")
                        .WithMany("Orders")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.City", "City")
                        .WithMany("Orders")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.Govern", "Govern")
                        .WithMany("Orders")
                        .HasForeignKey("GovernID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.Seller", "Seller")
                        .WithMany("Orders")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.TypeOfCharge", "TypeOfCharge")
                        .WithMany("Orders")
                        .HasForeignKey("TypeOfChargeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.TypeOfPayment", "TypeOfPayment")
                        .WithMany("Orders")
                        .HasForeignKey("TypeOfPaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.TypeOfReceipt", "TypeOfReceipt")
                        .WithMany("Orders")
                        .HasForeignKey("TypeOfReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("City");

                    b.Navigation("Govern");

                    b.Navigation("OrderStatus");

                    b.Navigation("Seller");

                    b.Navigation("TypeOfCharge");

                    b.Navigation("TypeOfPayment");

                    b.Navigation("TypeOfReceipt");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Product", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.SpecialCharge", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.City", "City")
                        .WithMany("SpecialCharges")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.Seller", "Seller")
                        .WithMany("SpecialCharges")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Access_Layer.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Seller", b =>
                {
                    b.HasOne("Data_Access_Layer.Entity.City", "StoreCity")
                        .WithMany("Sellers")
                        .HasForeignKey("StoreCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StoreCity");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Branch", b =>
                {
                    b.Navigation("Agents");

                    b.Navigation("ApplicationUsers");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.City", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Sellers");

                    b.Navigation("SpecialCharges");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.FieldJob", b =>
                {
                    b.Navigation("FieldPrivilege");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Govern", b =>
                {
                    b.Navigation("Agents");

                    b.Navigation("Cities");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Privilege", b =>
                {
                    b.Navigation("FieldPrivilege");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfCharge", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfOffer", b =>
                {
                    b.Navigation("Agents");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfPayment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.TypeOfReceipt", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data_Access_Layer.Entity.Seller", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("SpecialCharges");
                });
#pragma warning restore 612, 618
        }
    }
}
