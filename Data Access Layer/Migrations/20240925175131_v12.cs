using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 408, DateTimeKind.Local).AddTicks(3544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 8, DateTimeKind.Local).AddTicks(669));

            migrationBuilder.AddColumn<string>(
                name: "ReasonOfReject",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "chargeCost",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 408, DateTimeKind.Local).AddTicks(848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 7, DateTimeKind.Local).AddTicks(4478));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 407, DateTimeKind.Local).AddTicks(8180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 6, DateTimeKind.Local).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc2654f2-72b1-4e86-a70a-eab622dc0ac7", "AQAAAAIAAYagAAAAELzyGxeNenbIrc8Nuc0hDcd7ECo2sQfB3nf6zS0Hjck8zs8ytCwX81gc9IJGTdYcuw==", "1256d92b-52c7-411c-b33e-0b7f789b077c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonOfReject",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "chargeCost",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 8, DateTimeKind.Local).AddTicks(669),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 408, DateTimeKind.Local).AddTicks(3544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 7, DateTimeKind.Local).AddTicks(4478),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 408, DateTimeKind.Local).AddTicks(848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 23, 3, 46, 6, DateTimeKind.Local).AddTicks(8091),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 25, 20, 51, 25, 407, DateTimeKind.Local).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f76a459f-198f-4942-82c8-a2f1e617df84", "AQAAAAIAAYagAAAAELSix7VHICH8EN4VDsVk4H6HR6QcF652iJhrrWhY23nWKjztsq/GS17WSQZyp/ElMA==", "34c8a389-5127-4d0e-abb5-6bd284ab3d81" });
        }
    }
}
