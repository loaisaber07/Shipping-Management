using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addAgantUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(6110),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(6911));

            migrationBuilder.AddColumn<string>(
                name: "AgentID",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(3198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(2853));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(214),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 60, DateTimeKind.Local).AddTicks(9885));

            migrationBuilder.AddColumn<int>(
                name: "GovernID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThePrecentageOfCompanyFromOffer",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfOfferID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_AgentID",
                table: "Order",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers",
                column: "GovernID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TypeOfOfferID",
                table: "AspNetUsers",
                column: "TypeOfOfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers",
                column: "GovernID",
                principalTable: "governs",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_typeOfOffers_TypeOfOfferID",
                table: "AspNetUsers",
                column: "TypeOfOfferID",
                principalTable: "typeOfOffers",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order",
                column: "AgentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_typeOfOffers_TypeOfOfferID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_AgentID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AgentID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TypeOfOfferID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AgentID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThePrecentageOfCompanyFromOffer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TypeOfOfferID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(6911),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdding",
                table: "fieldJobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 61, DateTimeKind.Local).AddTicks(2853),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdding",
                table: "branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 20, 19, 2, 34, 60, DateTimeKind.Local).AddTicks(9885),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 21, 20, 13, 17, 457, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    GovernID = table.Column<int>(type: "int", nullable: false),
                    TypeOfOfferID = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThePrecentageOfCompanyFromOffer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_agents_branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_agents_governs_GovernID",
                        column: x => x.GovernID,
                        principalTable: "governs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_agents_typeOfOffers_TypeOfOfferID",
                        column: x => x.TypeOfOfferID,
                        principalTable: "typeOfOffers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agents_BranchID",
                table: "agents",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_agents_GovernID",
                table: "agents",
                column: "GovernID");

            migrationBuilder.CreateIndex(
                name: "IX_agents_TypeOfOfferID",
                table: "agents",
                column: "TypeOfOfferID");
        }
    }
}
