using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addTypeOfReceiptTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 976, DateTimeKind.Local).AddTicks(4296),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 614, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.AddColumn<int>(
                name: "TypeOfReceiptID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 976, DateTimeKind.Local).AddTicks(1951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 613, DateTimeKind.Local).AddTicks(7728));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 975, DateTimeKind.Local).AddTicks(8929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 612, DateTimeKind.Local).AddTicks(9741));

            migrationBuilder.CreateTable(
                name: "typeOfReceipts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeOfReceipts", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_TypeOfReceiptID",
                table: "Order",
                column: "TypeOfReceiptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_typeOfReceipts_TypeOfReceiptID",
                table: "Order",
                column: "TypeOfReceiptID",
                principalTable: "typeOfReceipts",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_typeOfReceipts_TypeOfReceiptID",
                table: "Order");

            migrationBuilder.DropTable(
                name: "typeOfReceipts");

            migrationBuilder.DropIndex(
                name: "IX_Order_TypeOfReceiptID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TypeOfReceiptID",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 614, DateTimeKind.Local).AddTicks(4531),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 976, DateTimeKind.Local).AddTicks(4296));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 613, DateTimeKind.Local).AddTicks(7728),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 976, DateTimeKind.Local).AddTicks(1951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 3, 37, 23, 612, DateTimeKind.Local).AddTicks(9741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 6, 22, 975, DateTimeKind.Local).AddTicks(8929));
        }
    }
}
