using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_privileges_fieldJobs_FieldJobID",
                table: "privileges");

            migrationBuilder.DropIndex(
                name: "IX_privileges_FieldJobID",
                table: "privileges");

            migrationBuilder.DropColumn(
                name: "Add",
                table: "privileges");

            migrationBuilder.DropColumn(
                name: "Delete",
                table: "privileges");

            migrationBuilder.DropColumn(
                name: "Display",
                table: "privileges");

            migrationBuilder.DropColumn(
                name: "Edit",
                table: "privileges");

            migrationBuilder.DropColumn(
                name: "FieldJobID",
                table: "privileges");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 17, 51, 0, 803, DateTimeKind.Local).AddTicks(2958),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 11, 17, 26, 13, 447, DateTimeKind.Local).AddTicks(6502));

            migrationBuilder.CreateTable(
                name: "fieldPrivileges",
                columns: table => new
                {
                    FieldJobID = table.Column<int>(type: "int", nullable: false),
                    PrivilegeID = table.Column<int>(type: "int", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Display = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fieldPrivileges", x => new { x.PrivilegeID, x.FieldJobID });
                    table.ForeignKey(
                        name: "FK_fieldPrivileges_fieldJobs_FieldJobID",
                        column: x => x.FieldJobID,
                        principalTable: "fieldJobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_fieldPrivileges_privileges_PrivilegeID",
                        column: x => x.PrivilegeID,
                        principalTable: "privileges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fieldPrivileges_FieldJobID",
                table: "fieldPrivileges",
                column: "FieldJobID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fieldPrivileges");

            migrationBuilder.AddColumn<bool>(
                name: "Add",
                table: "privileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "privileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Display",
                table: "privileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Edit",
                table: "privileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FieldJobID",
                table: "privileges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 17, 26, 13, 447, DateTimeKind.Local).AddTicks(6502),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 11, 17, 51, 0, 803, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.CreateIndex(
                name: "IX_privileges_FieldJobID",
                table: "privileges",
                column: "FieldJobID");

            migrationBuilder.AddForeignKey(
                name: "FK_privileges_fieldJobs_FieldJobID",
                table: "privileges",
                column: "FieldJobID",
                principalTable: "fieldJobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
