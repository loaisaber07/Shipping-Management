using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FiledJobID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GovernID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PickUp",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValueOfRejectedOrder",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "fieldJobs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fieldJobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_fieldJobs_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "weights",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultWeight = table.Column<int>(type: "int", nullable: false),
                    AdditionalWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weights", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "privileges",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Display = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    FieldJobID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_privileges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_privileges_fieldJobs_FieldJobID",
                        column: x => x.FieldJobID,
                        principalTable: "fieldJobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThePrecentageOfCompanyFromOffer = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    GovernID = table.Column<int>(type: "int", nullable: false),
                    TypeOfOfferID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "typeOfOffers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeOfOffers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_typeOfOffers_agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "agents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_branches_agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "agents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalCharge = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpCharge = table.Column<int>(type: "int", nullable: false),
                    SpecialChargeForSeller = table.Column<int>(type: "int", nullable: true),
                    GovernID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "governs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgentID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_governs_AspNetUsers_SellerID",
                        column: x => x.SellerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_governs_agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "agents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientNumber = table.Column<int>(type: "int", nullable: false),
                    ClientNumber2 = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    IsForVillage = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    VillageOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    GovernID = table.Column<int>(type: "int", nullable: false),
                    SellerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeOfPaymentID = table.Column<int>(type: "int", nullable: false),
                    TypeOfChargeID = table.Column<int>(type: "int", nullable: false),
                    ProductStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_products_AspNetUsers_SellerID",
                        column: x => x.SellerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_governs_GovernID",
                        column: x => x.GovernID,
                        principalTable: "governs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_productStatuses_products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "typeOfCharges",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeOfCharges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_typeOfCharges_products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "typeOfPayments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typeOfPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_typeOfPayments_products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FiledJobID",
                table: "AspNetUsers",
                column: "FiledJobID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers",
                column: "GovernID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProductID",
                table: "AspNetUsers",
                column: "ProductID");

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

            migrationBuilder.CreateIndex(
                name: "IX_branches_AgentID",
                table: "branches",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_branches_ProductID",
                table: "branches",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernID",
                table: "Cities",
                column: "GovernID");

            migrationBuilder.CreateIndex(
                name: "IX_fieldJobs_ApplicationUserID",
                table: "fieldJobs",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_governs_AgentID",
                table: "governs",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_governs_ProductID",
                table: "governs",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_governs_SellerID",
                table: "governs",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_privileges_FieldJobID",
                table: "privileges",
                column: "FieldJobID");

            migrationBuilder.CreateIndex(
                name: "IX_products_BranchID",
                table: "products",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_products_GovernID",
                table: "products",
                column: "GovernID");

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductStatusID",
                table: "products",
                column: "ProductStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_products_SellerID",
                table: "products",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_products_TypeOfChargeID",
                table: "products",
                column: "TypeOfChargeID");

            migrationBuilder.CreateIndex(
                name: "IX_products_TypeOfPaymentID",
                table: "products",
                column: "TypeOfPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_productStatuses_ProductID",
                table: "productStatuses",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_typeOfCharges_ProductID",
                table: "typeOfCharges",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_typeOfOffers_AgentID",
                table: "typeOfOffers",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_typeOfPayments_ProductID",
                table: "typeOfPayments",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_fieldJobs_FiledJobID",
                table: "AspNetUsers",
                column: "FiledJobID",
                principalTable: "fieldJobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers",
                column: "GovernID",
                principalTable: "governs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_products_ProductID",
                table: "AspNetUsers",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agents_branches_BranchID",
                table: "agents",
                column: "BranchID",
                principalTable: "branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agents_governs_GovernID",
                table: "agents",
                column: "GovernID",
                principalTable: "governs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agents_typeOfOffers_TypeOfOfferID",
                table: "agents",
                column: "TypeOfOfferID",
                principalTable: "typeOfOffers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_branches_products_ProductID",
                table: "branches",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_governs_GovernID",
                table: "Cities",
                column: "GovernID",
                principalTable: "governs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_governs_products_ProductID",
                table: "governs",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productStatuses_ProductStatusID",
                table: "products",
                column: "ProductStatusID",
                principalTable: "productStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_typeOfCharges_TypeOfChargeID",
                table: "products",
                column: "TypeOfChargeID",
                principalTable: "typeOfCharges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_typeOfPayments_TypeOfPaymentID",
                table: "products",
                column: "TypeOfPaymentID",
                principalTable: "typeOfPayments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_fieldJobs_FiledJobID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_governs_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_products_ProductID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_agents_branches_BranchID",
                table: "agents");

            migrationBuilder.DropForeignKey(
                name: "FK_products_branches_BranchID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_agents_governs_GovernID",
                table: "agents");

            migrationBuilder.DropForeignKey(
                name: "FK_products_governs_GovernID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_agents_typeOfOffers_TypeOfOfferID",
                table: "agents");

            migrationBuilder.DropForeignKey(
                name: "FK_productStatuses_products_ProductID",
                table: "productStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_typeOfCharges_products_ProductID",
                table: "typeOfCharges");

            migrationBuilder.DropForeignKey(
                name: "FK_typeOfPayments_products_ProductID",
                table: "typeOfPayments");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "privileges");

            migrationBuilder.DropTable(
                name: "weights");

            migrationBuilder.DropTable(
                name: "fieldJobs");

            migrationBuilder.DropTable(
                name: "branches");

            migrationBuilder.DropTable(
                name: "governs");

            migrationBuilder.DropTable(
                name: "typeOfOffers");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "productStatuses");

            migrationBuilder.DropTable(
                name: "typeOfCharges");

            migrationBuilder.DropTable(
                name: "typeOfPayments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FiledJobID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProductID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FiledJobID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GovernID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PickUp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ValueOfRejectedOrder",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
