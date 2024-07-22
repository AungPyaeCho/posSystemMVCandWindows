using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using posSystem.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            // Fetch data for sales chart
            var salesData = _context.Sales
                .Where(s => !string.IsNullOrEmpty(s.saleDate)) // Ensure saleDate is not null or empty
                .AsEnumerable() // Switch to client-side evaluation
                .GroupBy(s => DateTime.Parse(s.saleDate).Date) // Group by Date part of DateTime
                .Select(g => new SalesDataViewModel
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(s => s.totalAmount ?? 0)
                })
                .OrderBy(d => d.Date)
                .ToList();

            // Fetch data for members chart
            var membersData = await _context.Members
                .GroupBy(m => m.memberLevel)
                .Select(g => new MembersDataViewModel
                {
                    Level = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // Fetch data for staff chart
            var staffData = await _context.Staffs
                .GroupBy(s => s.staffRole)
                .Select(g => new StaffDataViewModel
                {
                    Role = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // Prepare the view model
            var viewModel = new DashboardViewModel
            {
                SalesData = salesData,
                MembersData = membersData,
                StaffData = staffData,
                TotalSales = salesData.Sum(s => s.TotalAmount),
                TotalItems = await _context.Items.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                TotalBrands = await _context.Brands.CountAsync(),
                RecentSales = await _context.Sales.OrderByDescending(s => s.saleDate).Take(5).ToListAsync(),
                RecentActivities = new List<string> // Dummy data for recent activities
                {
                    "Activity 1",
                    "Activity 2",
                    "Activity 3"
                }
            };

            return View(viewModel);
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