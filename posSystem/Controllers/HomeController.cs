using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using posSystem.ViewModels;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace posSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalSales = await _context.Sales.SumAsync(s => s.totalAmount ?? 0);
            var totalItems = await _context.Items.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var totalBrands = await _context.Brands.CountAsync();

            // Fetch recent sales
            var recentSales = await _context.Sales
                .OrderByDescending(s => s.saleDate)
                .Take(5)
                .ToListAsync();

            // Fetch sales data for the chart (e.g., total sales per month)
            var salesData = await _context.Sales
                .GroupBy(s => s.saleDate.Value.Month)
                .Select(g => new SalesDataViewModel
                {
                    Date = new DateTime(DateTime.Now.Year, g.Key, 1),
                    TotalAmount = g.Sum(s => s.totalAmount ?? 0)
                })
                .ToListAsync();

            // Example: Fetch recent activities (you can replace this with actual logic)
            var recentActivities = new List<string>
    {
        "User X added a new item.",
        "Category Y was updated.",
        "Sale Z was processed."
    };

            // Populate the DashboardViewModel
            var dashboardViewModel = new DashboardViewModel
            {
                TotalSales = totalSales,
                TotalItems = totalItems,
                TotalCategories = totalCategories,
                TotalBrands = totalBrands,
                RecentSales = recentSales,
                RecentActivities = recentActivities,
                SalesData = salesData,
                MembersData = null, // Add logic to populate this if necessary
                StaffData = null // Add logic to populate this if necessary
            };

            return View("Index", dashboardViewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
