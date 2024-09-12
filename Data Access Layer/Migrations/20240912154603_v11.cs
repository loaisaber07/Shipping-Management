using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 46, 2, 573, DateTimeKind.Local).AddTicks(5757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(4682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 46, 2, 572, DateTimeKind.Local).AddTicks(7872),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(664));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(4682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 46, 2, 573, DateTimeKind.Local).AddTicks(5757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 12, 18, 41, 34, 425, DateTimeKind.Local).AddTicks(664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 12, 18, 46, 2, 572, DateTimeKind.Local).AddTicks(7872));
        }
    }
}
