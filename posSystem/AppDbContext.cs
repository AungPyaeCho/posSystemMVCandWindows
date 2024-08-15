using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System;
using System.Collections.Generic;
using posSystem.Middlewares;

namespace posSystem
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> _logger;

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
        public DbSet<LoginStaffDetailModel> LoginStaffDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                // Define foreign key relationships
                modelBuilder.Entity<SubCategoryModel>()
                    .HasOne(s => s.Category)
                    .WithMany()
                    .HasForeignKey(s => s.catId);

                modelBuilder.Entity<SubBrandModel>()
                    .HasOne(s => s.Brand)
                    .WithMany()
                    .HasForeignKey(s => s.brandId);

                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.Category)
                    .WithMany()
                    .HasForeignKey(i => i.catId);

                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.SubCategory)
                    .WithMany()
                    .HasForeignKey(i => i.subCId);

                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.Brand)
                    .WithMany()
                    .HasForeignKey(i => i.brandId);

                modelBuilder.Entity<ItemModel>()
                    .HasOne(i => i.SubBrand)
                    .WithMany()
                    .HasForeignKey(i => i.subBId);

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

                modelBuilder.Entity<LoginDetailModel>()
                    .HasOne(ld => ld.Admin)
                    .WithMany()
                    .HasForeignKey(ld => ld.adminId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<LoginStaffDetailModel>()
                .ToTable("tblLoginStaffDetail")
                .HasKey(l => l.ldId);

                modelBuilder.Entity<LoginStaffDetailModel>()
                    .HasOne(l => l.Staff)
                    .WithMany()
                    .HasForeignKey(l => l.staffId);

                // Seed default admin user
                string defaultAdminId = Guid.NewGuid().ToString();
                string defaultAdminEmail = "admin@pos.com";
                string defaultAdminPassword = SimpleEncryptionHelper.Encrypt("Admin@123"); // Encrypt the default password

                modelBuilder.Entity<AdminModel>().HasData(
                    new AdminModel
                    {
                        id = defaultAdminId,
                        adminName = "Default Admin",
                        adminEmail = defaultAdminEmail,
                        adminPassword = defaultAdminPassword,
                        adminCreateAt = DateTime.Now.ToString()
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while configuring the model");
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }
    }
}
