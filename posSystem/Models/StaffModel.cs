using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblStaff")]
    public class StaffModel
    {
        [Key]
        public string staffId { get; set; }
        public string? staffName { get; set; }
        public string? staffEmail { get; set; }
        public string? staffPhone { get; set; }
        public string? staffAddress { get; set; }
        public string? staffRole { get; set; }
        public string? staffPhoto { get; set; }
        public DateTime? staffCreatedAt { get; set; }
    }

    public class StaffResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<StaffModel> staffData { get; set; }
    }
}
