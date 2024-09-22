using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class nullableforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 111, DateTimeKind.Local).AddTicks(2296),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.AlterColumn<string>(
                name: "AgentID",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(9340),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(6229),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(6110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 111, DateTimeKind.Local).AddTicks(2296));

            migrationBuilder.AlterColumn<string>(
                name: "AgentID",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(3198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(214),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 38, 8, 110, DateTimeKind.Local).AddTicks(6229));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
