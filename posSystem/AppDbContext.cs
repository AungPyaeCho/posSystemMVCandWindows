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
        public DbSet<AdminModel> Admins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added && e.Entity is CategoryModel);

        //    foreach (var entry in entries)
        //    {
        //        ((CategoryModel)entry.Entity).catCreatedAt = DateTime.Now;
        //    }

        //    return base.SaveChanges();
        //}
    }
}
