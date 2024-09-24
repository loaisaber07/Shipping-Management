using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 8, DateTimeKind.Local).AddTicks(669),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 487, DateTimeKind.Local).AddTicks(9943));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 7, DateTimeKind.Local).AddTicks(4478),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 486, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 6, DateTimeKind.Local).AddTicks(8091),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 486, DateTimeKind.Local).AddTicks(492));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f76a459f-198f-4942-82c8-a2f1e617df84", "AQAAAAIAAYagAAAAELSix7VHICH8EN4VDsVk4H6HR6QcF652iJhrrWhY23nWKjztsq/GS17WSQZyp/ElMA==", "34c8a389-5127-4d0e-abb5-6bd284ab3d81" });

            migrationBuilder.InsertData(
                table: "typeOfReceipts",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Branch" },
                    { 2, "Store" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "typeOfReceipts",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "typeOfReceipts",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 487, DateTimeKind.Local).AddTicks(9943),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 8, DateTimeKind.Local).AddTicks(669));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 486, DateTimeKind.Local).AddTicks(8953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 7, DateTimeKind.Local).AddTicks(4478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 21, 45, 18, 486, DateTimeKind.Local).AddTicks(492),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 6, DateTimeKind.Local).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01a2c379-249e-4a68-85b3-379e98db5131", "AQAAAAIAAYagAAAAEAC9UHL7Gm+Vu4GAv2BCP/ZcUbD7YHd/vm/0+fO3CN45QWstjtLzIxI0dPsJsbohSQ==", "90242347-1b6d-42aa-a5cc-7594790affcc" });
        }
    }
}
