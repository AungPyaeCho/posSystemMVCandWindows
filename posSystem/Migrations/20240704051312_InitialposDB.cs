using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace posSystem.Migrations
{ 
    /// <inheritdoc />
    public partial class InitialposDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAdmin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminLoginAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminLoginName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    catId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catCreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catUpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catDeleteAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.catId);
                });

            migrationBuilder.CreateTable(
                name: "tblDiscount",
                columns: table => new
                {
                    disId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDiscount", x => x.disId);
                });

            migrationBuilder.CreateTable(
                name: "tblLoginDetail",
                columns: table => new
                {
                    ldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adminId = table.Column<int>(type: "int", nullable: true),
                    staffId = table.Column<int>(type: "int", nullable: true),
                    loginAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logOutAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoginDetail", x => x.ldId);
                });

            migrationBuilder.CreateTable(
                name: "tblMember",
                columns: table => new
                {
                    memberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    memberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMember", x => x.memberId);
                });

            migrationBuilder.CreateTable(
                name: "tblPromotion",
                columns: table => new
                {
                    proId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPromotion", x => x.proId);
                });

            migrationBuilder.CreateTable(
                name: "tblStaff",
                columns: table => new
                {
                    staffId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStaff", x => x.staffId);
                });

            migrationBuilder.CreateTable(
                name: "tblSubCategory",
                columns: table => new
                {
                    subCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subCatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatUpdateCount = table.Column<int>(type: "int", nullable: true),
                    catCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubCategory", x => x.subCId);
                    table.ForeignKey(
                        name: "FK_tblSubCategory_tblCategory_catId",
                        column: x => x.catId,
                        principalTable: "tblCategory",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSale",
                columns: table => new
                {
                    saleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    memberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    saleQty = table.Column<int>(type: "int", nullable: true),
                    totalAmount = table.Column<int>(type: "int", nullable: true),
                    saleDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    promotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    discount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSale", x => x.saleId);
                    table.ForeignKey(
                        name: "FK_tblSale_tblMember_memberId",
                        column: x => x.memberId,
                        principalTable: "tblMember",
                        principalColumn: "memberId");
                    table.ForeignKey(
                        name: "FK_tblSale_tblStaff_staffId",
                        column: x => x.staffId,
                        principalTable: "tblStaff",
                        principalColumn: "staffId");
                });

            migrationBuilder.CreateTable(
                name: "tblItem",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemCategory = table.Column<int>(type: "int", nullable: true),
                    itemSubCategory = table.Column<int>(type: "int", nullable: true),
                    itemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemBarcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemStock = table.Column<int>(type: "int", nullable: true),
                    itemSold = table.Column<int>(type: "int", nullable: true),
                    itemRemainStock = table.Column<int>(type: "int", nullable: true),
                    itemCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemUpdateCount = table.Column<int>(type: "int", nullable: true),
                    creatorName = table.Column<int>(type: "int", nullable: true),
                    catId = table.Column<int>(type: "int", nullable: false),
                    subCId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItem", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_tblItem_tblCategory_itemCategory",
                        column: x => x.itemCategory,
                        principalTable: "tblCategory",
                        principalColumn: "catId");
                    table.ForeignKey(
                        name: "FK_tblItem_tblSubCategory_itemSubCategory",
                        column: x => x.itemSubCategory,
                        principalTable: "tblSubCategory",
                        principalColumn: "subCId");
                });

            migrationBuilder.CreateTable(
                name: "tblSaleDetail",
                columns: table => new
                {
                    sdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saleId = table.Column<int>(type: "int", nullable: true),
                    itemId = table.Column<int>(type: "int", nullable: true),
                    saleQuantity = table.Column<int>(type: "int", nullable: true),
                    totalQty = table.Column<int>(type: "int", nullable: true),
                    itemPrice = table.Column<int>(type: "int", nullable: true),
                    totalAmount = table.Column<int>(type: "int", nullable: true),
                    saleDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSaleDetail", x => x.sdId);
                    table.ForeignKey(
                        name: "FK_tblSaleDetail_tblItem_itemId",
                        column: x => x.itemId,
                        principalTable: "tblItem",
                        principalColumn: "itemId");
                    table.ForeignKey(
                        name: "FK_tblSaleDetail_tblSale_saleId",
                        column: x => x.saleId,
                        principalTable: "tblSale",
                        principalColumn: "saleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_itemCategory",
                table: "tblItem",
                column: "itemCategory");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_itemSubCategory",
                table: "tblItem",
                column: "itemSubCategory");

            migrationBuilder.CreateIndex(
                name: "IX_tblSale_memberId",
                table: "tblSale",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSale_staffId",
                table: "tblSale",
                column: "staffId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSaleDetail_itemId",
                table: "tblSaleDetail",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSaleDetail_saleId",
                table: "tblSaleDetail",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_catId",
                table: "tblSubCategory",
                column: "catId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAdmin");

            migrationBuilder.DropTable(
                name: "tblDiscount");

            migrationBuilder.DropTable(
                name: "tblLoginDetail");

            migrationBuilder.DropTable(
                name: "tblPromotion");

            migrationBuilder.DropTable(
                name: "tblSaleDetail");

            migrationBuilder.DropTable(
                name: "tblItem");

            migrationBuilder.DropTable(
                name: "tblSale");

            migrationBuilder.DropTable(
                name: "tblSubCategory");

            migrationBuilder.DropTable(
                name: "tblMember");

            migrationBuilder.DropTable(
                name: "tblStaff");

            migrationBuilder.DropTable(
                name: "tblCategory");
        }
    }
}
