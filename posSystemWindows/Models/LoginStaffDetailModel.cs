using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace posSystemWindows.Models
{
    public class LoginStaffDetailModel
    {
        public int ldId { get; set; }
        public int? staffId { get; set; }
        public string? staffName { get; set; }
        public string? staffCode { get; set; }
        public string? staffRole { get; set; }
        public string? sessionId { get; set; }
        public DateTime? sessionExpired { get; set; }
        public DateTime? loginAt { get; set; }
        public DateTime? logOutAt { get; set; }
    }
}
