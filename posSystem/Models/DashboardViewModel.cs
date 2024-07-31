using posSystem.Models;
using System;
using System.Collections.Generic;

namespace posSystem.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalSales { get; set; }
        public int TotalItems { get; set; }
        public int TotalCategories { get; set; }
        public int TotalBrands { get; set; }
        public List<SaleModel>? RecentSales { get; set; }
        public List<string>? RecentActivities { get; set; }
        public List<SalesDataViewModel>? SalesData { get; set; }
        public List<MembersDataViewModel>? MembersData { get; set; }
        public List<StaffDataViewModel>? StaffData { get; set; }
    }

    public class SalesDataViewModel
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MembersDataViewModel
    {
        public string? Level { get; set; }
        public int Count { get; set; }
    }

    public class StaffDataViewModel
    {
        public string? Role { get; set; }
        public int Count { get; set; }
    }
}
