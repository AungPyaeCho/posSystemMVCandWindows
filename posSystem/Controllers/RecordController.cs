using Microsoft.AspNetCore.Mvc;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class RecordController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RecordController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public IActionResult RecordIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Admin.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<LoginStaffDetailModel> list = _appDbContext.LoginStaffDetails
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            LoginStaffDetailResponseModel response = new()
            {
                loginStaffData = list,
                pageSize = pageSize,
                pageCount = pageCount,
                pageNo = pageNo
            };
            return View("RecordIndex", response);
        }
    }
}
