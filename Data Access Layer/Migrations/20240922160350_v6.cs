using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 460, DateTimeKind.Local).AddTicks(1925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 111, DateTimeKind.Local).AddTicks(2296));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 459, DateTimeKind.Local).AddTicks(5580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 458, DateTimeKind.Local).AddTicks(8312),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(6229));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 111, DateTimeKind.Local).AddTicks(2296),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 460, DateTimeKind.Local).AddTicks(1925));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(9340),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 459, DateTimeKind.Local).AddTicks(5580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(6229),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 19, 3, 49, 458, DateTimeKind.Local).AddTicks(8312));
        }
    }
}
