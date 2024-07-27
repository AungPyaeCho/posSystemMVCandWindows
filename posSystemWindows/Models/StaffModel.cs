using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class StaffModel
    {
        public int staffId { get; set; }
        public string? staffCode { get; set; }
        public string? staffName { get; set; }
        public string? staffEmail { get; set; }
        public string? staffPassword { get; set; }
        public string? staffPhone { get; set; }
        public string? staffAddress { get; set; }
        public string? staffRole { get; set; }
        public string? staffPhoto { get; set; }
        public DateTime? staffJoinedDate { get; set; }
        public string? staffCreateAt { get; set; }
        public string? staffUpdateAt { get; set; }
        public int? staffUpdateCount { get; set; }
    }
}
