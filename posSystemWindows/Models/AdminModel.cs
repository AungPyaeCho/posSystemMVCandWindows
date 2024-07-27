using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class AdminModel
    {
        public string? id { get; set; }
        public string? adminName { get; set; }
        public string? adminEmail { get; set; }
        public string? adminPassword { get; set; }
        public string? adminCreateAt { get; set; }
    }

    public class loginResponeModel
    {
        public bool IsSuccess { get; set; }
        public string responeMsg { get; set; }
    }

    public class AdminResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<AdminModel> adminData { get; set; }
    }
}
