﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using posSystem;

#nullable disable

namespace posSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240715073536_defaultadmin")]
    partial class defaultadmin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("posSystem.Models.AdminModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("adminCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adminPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblAdmin");

                    b.HasData(
                        new
                        {
                            id = "4dc7d508-b960-4edf-9103-b0ba83af4bbb",
                            adminCreateAt = "7/15/2024 2:05:35 PM",
                            adminEmail = "admin@pos.com",
                            adminName = "Default Admin",
                            adminPassword = "QWRtaW5AMTIz"
                        });
                });

            modelBuilder.Entity("posSystem.Models.BrandModel", b =>
                {
                    b.Property<int>("brandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("brandId"));

                    b.Property<string>("brandCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brandCreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brandDeleteAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brandDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("brandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("brandUpdateCount")
                        .HasColumnType("int");

                    b.Property<string>("brandUpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("brandId");

                    b.ToTable("tblBrand");
                });

            modelBuilder.Entity("posSystem.Models.CategoryModel", b =>
                {
                    b.Property<int>("catId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("catId"));

                    b.Property<string>("catCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("catCreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("catDeleteAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("catDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("catName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("catUpdateCount")
                        .HasColumnType("int");

                    b.Property<string>("catUpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("catId");

                    b.ToTable("tblCategory");
                });

            modelBuilder.Entity("posSystem.Models.DiscountModel", b =>
                {
                    b.Property<int>("disId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("disId"));

                    b.Property<string>("disCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("disDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("disName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("disUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("disUpdateCount")
                        .HasColumnType("int");

                    b.HasKey("disId");

                    b.ToTable("tblDiscount");
                });

            modelBuilder.Entity("posSystem.Models.ItemModel", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("itemId"));

                    b.Property<string>("brandCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("brandId")
                        .HasColumnType("int");

                    b.Property<string>("catCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("catId")
                        .HasColumnType("int");

                    b.Property<string>("creatorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemBarcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemBrand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("itemBuyPrice")
                        .HasColumnType("int");

                    b.Property<string>("itemCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("itemRemainStock")
                        .HasColumnType("int");

                    b.Property<int?>("itemSalePrice")
                        .HasColumnType("int");

                    b.Property<int?>("itemSold")
                        .HasColumnType("int");

                    b.Property<int?>("itemStock")
                        .HasColumnType("int");

                    b.Property<string>("itemSubBrnad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemSubCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("itemUpdateCount")
                        .HasColumnType("int");

                    b.Property<int?>("itemWholeSalePrice")
                        .HasColumnType("int");

                    b.Property<int?>("subBId")
                        .HasColumnType("int");

                    b.Property<string>("subBrandCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("subCId")
                        .HasColumnType("int");

                    b.Property<string>("subCatCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("itemId");

                    b.HasIndex("brandId");

                    b.HasIndex("catId");

                    b.HasIndex("subBId");

                    b.HasIndex("subCId");

                    b.ToTable("tblItem");
                });

            modelBuilder.Entity("posSystem.Models.LoginDetailModel", b =>
                {
                    b.Property<string>("ldId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("adminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("logOutAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loginAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("sessionExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("sessionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ldId");

                    b.HasIndex("adminId");

                    b.ToTable("tblLoginDetail");
                });

            modelBuilder.Entity("posSystem.Models.MemberModel", b =>
                {
                    b.Property<string>("memberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("memberAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("memberPoints")
                        .HasColumnType("int");

                    b.Property<string>("memberUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("memberUpdateCount")
                        .HasColumnType("int");

                    b.Property<int>("memberUsedPoints")
                        .HasColumnType("int");

                    b.HasKey("memberId");

                    b.ToTable("tblMember");
                });

            modelBuilder.Entity("posSystem.Models.PromotionModel", b =>
                {
                    b.Property<int>("proId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("proId"));

                    b.Property<string>("proCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("proCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("proDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("proName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("proUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("proUpdateCount")
                        .HasColumnType("int");

                    b.HasKey("proId");

                    b.ToTable("tblPromotion");
                });

            modelBuilder.Entity("posSystem.Models.SaleDetailModel", b =>
                {
                    b.Property<int>("sdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sdId"));

                    b.Property<int?>("itemId")
                        .HasColumnType("int");

                    b.Property<int?>("itemPrice")
                        .HasColumnType("int");

                    b.Property<string>("saleDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("saleId")
                        .HasColumnType("int");

                    b.Property<int?>("saleQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("totalAmount")
                        .HasColumnType("int");

                    b.Property<int?>("totalQty")
                        .HasColumnType("int");

                    b.HasKey("sdId");

                    b.HasIndex("itemId");

                    b.HasIndex("saleId");

                    b.ToTable("tblSaleDetail");
                });

            modelBuilder.Entity("posSystem.Models.SaleModel", b =>
                {
                    b.Property<int>("saleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("saleId"));

                    b.Property<string>("discount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("memberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("promotion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("saleDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("saleQty")
                        .HasColumnType("int");

                    b.Property<string>("staffId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("staffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("totalAmount")
                        .HasColumnType("int");

                    b.HasKey("saleId");

                    b.HasIndex("memberId");

                    b.HasIndex("staffId");

                    b.ToTable("tblSale");
                });

            modelBuilder.Entity("posSystem.Models.StaffModel", b =>
                {
                    b.Property<string>("staffId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("staffAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("staffJoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("staffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("staffUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("staffUpdateCount")
                        .HasColumnType("int");

                    b.HasKey("staffId");

                    b.ToTable("tblStaff");
                });

            modelBuilder.Entity("posSystem.Models.SubBrandModel", b =>
                {
                    b.Property<int>("subBId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subBId"));

                    b.Property<string>("brandCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("brandId")
                        .HasColumnType("int");

                    b.Property<string>("brandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subBrandCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subBrandCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subBrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subBrandUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("subBrandUpdateCount")
                        .HasColumnType("int");

                    b.HasKey("subBId");

                    b.HasIndex("brandId");

                    b.ToTable("tblSubBrand");
                });

            modelBuilder.Entity("posSystem.Models.SubCategoryModel", b =>
                {
                    b.Property<int>("subCId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subCId"));

                    b.Property<string>("catCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("catId")
                        .HasColumnType("int");

                    b.Property<string>("catName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subCatCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subCatCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subCatName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subCatUpdateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("subCatUpdateCount")
                        .HasColumnType("int");

                    b.HasKey("subCId");

                    b.HasIndex("catId");

                    b.ToTable("tblSubCategory");
                });

            modelBuilder.Entity("posSystem.Models.ItemModel", b =>
                {
                    b.HasOne("posSystem.Models.BrandModel", "Brand")
                        .WithMany()
                        .HasForeignKey("brandId");

                    b.HasOne("posSystem.Models.CategoryModel", "Category")
                        .WithMany()
                        .HasForeignKey("catId");

                    b.HasOne("posSystem.Models.SubBrandModel", "SubBrand")
                        .WithMany()
                        .HasForeignKey("subBId");

                    b.HasOne("posSystem.Models.SubCategoryModel", "SubCategory")
                        .WithMany()
                        .HasForeignKey("subCId");

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("SubBrand");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("posSystem.Models.LoginDetailModel", b =>
                {
                    b.HasOne("posSystem.Models.AdminModel", "Admin")
                        .WithMany()
                        .HasForeignKey("adminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("posSystem.Models.SaleDetailModel", b =>
                {
                    b.HasOne("posSystem.Models.ItemModel", "Item")
                        .WithMany()
                        .HasForeignKey("itemId");

                    b.HasOne("posSystem.Models.SaleModel", "Sale")
                        .WithMany("saleDetails")
                        .HasForeignKey("saleId");

                    b.Navigation("Item");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("posSystem.Models.SaleModel", b =>
                {
                    b.HasOne("posSystem.Models.MemberModel", "Member")
                        .WithMany()
                        .HasForeignKey("memberId");

                    b.HasOne("posSystem.Models.StaffModel", "Staff")
                        .WithMany()
                        .HasForeignKey("staffId");

                    b.Navigation("Member");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("posSystem.Models.SubBrandModel", b =>
                {
                    b.HasOne("posSystem.Models.BrandModel", "Brand")
                        .WithMany()
                        .HasForeignKey("brandId");

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("posSystem.Models.SubCategoryModel", b =>
                {
                    b.HasOne("posSystem.Models.CategoryModel", "Category")
                        .WithMany()
                        .HasForeignKey("catId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("posSystem.Models.SaleModel", b =>
                {
                    b.Navigation("saleDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
