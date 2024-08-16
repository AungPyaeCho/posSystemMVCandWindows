using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace posSystem.Migrations
{
    /// <inheritdoc />
    public partial class posdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAdmin",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    adminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tblBrand",
                columns: table => new
                {
                    brandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandCreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandUpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandDeleteAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBrand", x => x.brandId);
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
                    catUpdateCount = table.Column<int>(type: "int", nullable: true),
                    catDeleteAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    disValue = table.Column<int>(type: "int", nullable: true),
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
                name: "tblMember",
                columns: table => new
                {
                    memberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    memberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    memberPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberPoints = table.Column<int>(type: "int", nullable: true),
                    memberUsedPoints = table.Column<int>(type: "int", nullable: false),
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
                    staffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffJoinedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    staffCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStaff", x => x.staffId);
                });

            migrationBuilder.CreateTable(
                name: "tblLoginDetail",
                columns: table => new
                {
                    ldId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    adminId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    adminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    loginAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logOutAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoginDetail", x => x.ldId);
                    table.ForeignKey(
                        name: "FK_tblLoginDetail_tblAdmin_adminId",
                        column: x => x.adminId,
                        principalTable: "tblAdmin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSubBrand",
                columns: table => new
                {
                    subBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subBrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subBrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandId = table.Column<int>(type: "int", nullable: true),
                    brandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subBrandCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subBrandUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subBrandUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubBrand", x => x.subBId);
                    table.ForeignKey(
                        name: "FK_tblSubBrand_tblBrand_brandId",
                        column: x => x.brandId,
                        principalTable: "tblBrand",
                        principalColumn: "brandId");
                });

            migrationBuilder.CreateTable(
                name: "tblSubCategory",
                columns: table => new
                {
                    subCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subCatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catId = table.Column<int>(type: "int", nullable: true),
                    catCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCatUpdateCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSubCategory", x => x.subCId);
                    table.ForeignKey(
                        name: "FK_tblSubCategory_tblCategory_catId",
                        column: x => x.catId,
                        principalTable: "tblCategory",
                        principalColumn: "catId");
                });

            migrationBuilder.CreateTable(
                name: "tblLoginStaffDetail",
                columns: table => new
                {
                    ldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staffId = table.Column<int>(type: "int", nullable: true),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    loginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    logOutAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoginStaffDetail", x => x.ldId);
                    table.ForeignKey(
                        name: "FK_tblLoginStaffDetail_tblStaff_staffId",
                        column: x => x.staffId,
                        principalTable: "tblStaff",
                        principalColumn: "staffId");
                });

            migrationBuilder.CreateTable(
                name: "tblSale",
                columns: table => new
                {
                    saleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffId = table.Column<int>(type: "int", nullable: true),
                    staffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberId = table.Column<int>(type: "int", nullable: true),
                    memberCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    saleQty = table.Column<int>(type: "int", nullable: true),
                    totalAmount = table.Column<int>(type: "int", nullable: true),
                    receiveCash = table.Column<int>(type: "int", nullable: true),
                    refundCash = table.Column<int>(type: "int", nullable: true),
                    saleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    itemBuyPrice = table.Column<int>(type: "int", nullable: true),
                    itemSalePrice = table.Column<int>(type: "int", nullable: true),
                    itemWholeSalePrice = table.Column<int>(type: "int", nullable: true),
                    catId = table.Column<int>(type: "int", nullable: true),
                    catCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subCId = table.Column<int>(type: "int", nullable: true),
                    subCatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemSubCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brandId = table.Column<int>(type: "int", nullable: true),
                    brandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subBId = table.Column<int>(type: "int", nullable: true),
                    subBrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemSubBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemBarcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemStock = table.Column<int>(type: "int", nullable: true),
                    itemSold = table.Column<int>(type: "int", nullable: true),
                    itemRemainStock = table.Column<int>(type: "int", nullable: true),
                    itemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemCreateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemUpdateAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemUpdateCount = table.Column<int>(type: "int", nullable: true),
                    creatorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItem", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_tblItem_tblBrand_brandId",
                        column: x => x.brandId,
                        principalTable: "tblBrand",
                        principalColumn: "brandId");
                    table.ForeignKey(
                        name: "FK_tblItem_tblCategory_catId",
                        column: x => x.catId,
                        principalTable: "tblCategory",
                        principalColumn: "catId");
                    table.ForeignKey(
                        name: "FK_tblItem_tblSubBrand_subBId",
                        column: x => x.subBId,
                        principalTable: "tblSubBrand",
                        principalColumn: "subBId");
                    table.ForeignKey(
                        name: "FK_tblItem_tblSubCategory_subCId",
                        column: x => x.subCId,
                        principalTable: "tblSubCategory",
                        principalColumn: "subCId");
                });

            migrationBuilder.CreateTable(
                name: "tblSaleDetail",
                columns: table => new
                {
                    sdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    saleId = table.Column<int>(type: "int", nullable: true),
                    itemId = table.Column<int>(type: "int", nullable: true),
                    itemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    itemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saleQuantity = table.Column<int>(type: "int", nullable: true),
                    totalQty = table.Column<int>(type: "int", nullable: true),
                    itemPrice = table.Column<int>(type: "int", nullable: true),
                    totalAmount = table.Column<int>(type: "int", nullable: true),
                    saleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.InsertData(
                table: "tblAdmin",
                columns: new[] { "id", "adminCreateAt", "adminEmail", "adminName", "adminPassword" },
                values: new object[] { "7aec7fec-b126-4f49-a539-382f6a3346f8", "8/15/2024 4:08:05 PM", "admin@pos.com", "Default Admin", "QWRtaW5AMTIz" });

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_brandId",
                table: "tblItem",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_catId",
                table: "tblItem",
                column: "catId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_subBId",
                table: "tblItem",
                column: "subBId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_subCId",
                table: "tblItem",
                column: "subCId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLoginDetail_adminId",
                table: "tblLoginDetail",
                column: "adminId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLoginStaffDetail_staffId",
                table: "tblLoginStaffDetail",
                column: "staffId");

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
                name: "IX_tblSubBrand_brandId",
                table: "tblSubBrand",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSubCategory_catId",
                table: "tblSubCategory",
                column: "catId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDiscount");

            migrationBuilder.DropTable(
                name: "tblLoginDetail");

            migrationBuilder.DropTable(
                name: "tblLoginStaffDetail");

            migrationBuilder.DropTable(
                name: "tblPromotion");

            migrationBuilder.DropTable(
                name: "tblSaleDetail");

            migrationBuilder.DropTable(
                name: "tblAdmin");

            migrationBuilder.DropTable(
                name: "tblItem");

            migrationBuilder.DropTable(
                name: "tblSale");

            migrationBuilder.DropTable(
                name: "tblSubBrand");

            migrationBuilder.DropTable(
                name: "tblSubCategory");

            migrationBuilder.DropTable(
                name: "tblMember");

            migrationBuilder.DropTable(
                name: "tblStaff");

            migrationBuilder.DropTable(
                name: "tblBrand");

            migrationBuilder.DropTable(
                name: "tblCategory");
        }
    }
}
