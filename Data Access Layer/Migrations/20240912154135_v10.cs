using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GovernID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(4682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 15, 58, 15, 676, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 15, 58, 15, 674, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Govern",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Govern",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 15, 58, 15, 676, DateTimeKind.Local).AddTicks(7735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(4682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 15, 58, 15, 674, DateTimeKind.Local).AddTicks(9300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.AddColumn<int>(
                name: "GovernID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers",
                column: "GovernID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers",
                column: "GovernID",
                principalTable: "governs",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
