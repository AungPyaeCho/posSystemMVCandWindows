using Microsoft.EntityFrameworkCore;
using posSystem.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace posSystem
{
    public class AppDbContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SubCategoryModel> SubCategories { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<StaffModel> Staffs { get; set; }
        public DbSet<MemberModel> Members { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<SaleDetailModel> SaleDetails { get; set; }
        public DbSet<PromotionModel> Promotions { get; set; }
        public DbSet<DiscountModel> Discounts { get; set; }
        public DbSet<AdminModel> Admin { get; set; }
        public DbSet<LoginDetailModel> LoginDetails { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<SubBrandModel> SubBrands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationship between SubCategory and Category

            {
                // Define foreign key relationship between SubCategory and Category
                modelBuilder.Entity<SubCategoryModel>()
                    .HasOne(s => s.Category)
                    .WithMany()
                    .HasForeignKey(s => s.catId);

                modelBuilder.Entity<SubBrandModel>()
                    .HasOne(s => s.Brand)
                    .WithMany()
                    .HasForeignKey(s => s.brandId);

                // Define other relationships
                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.Category)
                    .WithMany()
                    .HasForeignKey(i => i.catId);

                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.SubCategory)
                    .WithMany()
                    .HasForeignKey(i => i.subCId);

                modelBuilder.Entity<SaleModel>()
                    .HasOne(s => s.Staff)
                    .WithMany()
                    .HasForeignKey(s => s.staffId);

                modelBuilder.Entity<SaleModel>()
                    .HasOne(s => s.Member)
                    .WithMany()
                    .HasForeignKey(s => s.memberId);

                modelBuilder.Entity<SaleDetailModel>()
                    .HasOne(sd => sd.Sale)
                    .WithMany(s => s.saleDetails)
                    .HasForeignKey(sd => sd.saleId);

                modelBuilder.Entity<SaleDetailModel>()
                    .HasOne(sd => sd.Item)
                    .WithMany()
                    .HasForeignKey(sd => sd.itemId);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
