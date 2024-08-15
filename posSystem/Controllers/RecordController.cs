using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace posSystem.Controllers
{
    public class RecordController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<RecordController> _logger;

        public RecordController(AppDbContext appDbContext, ILogger<RecordController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [ActionName("Index")]
        public IActionResult RecordIndex(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching records for page {PageNo} with page size {PageSize}.", pageNo, pageSize);

                int rowCount = _appDbContext.Admin.Count();
                int pageCount = rowCount / pageSize;

                if (rowCount % pageSize > 0)
                    pageCount++;

                var list = _appDbContext.LoginStaffDetails
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var response = new LoginStaffDetailResponseModel
                {
                    loginStaffData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                _logger.LogInformation("Successfully fetched {RecordCount} records.", list.Count);
                return View("RecordIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching records for page {PageNo}.", pageNo);
                // Handle the error by returning an appropriate view or message
                ModelState.AddModelError("", $"An error occurred while fetching records: {ex.Message}");
                return View("RecordIndex", new LoginStaffDetailResponseModel()); // Returning an empty model or an error view
            }
        }
    }
}
