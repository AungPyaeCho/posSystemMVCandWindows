using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace posSystemWindows.Models
{
    public class SubBrandModel
    {
        public int subBId { get; set; }
        public string? subBrandName { get; set; }
        public string? subBrandCode { get; set; }
        public int? brandId { get; set; }
        public string? brandCode { get; set; }
        public string? brandName { get; set; }
        public string? subBrandCreateAt { get; set; }
        public string? subBrandUpdateAt { get; set; }
        public int? subBrandUpdateCount { get; set; }
    }
}
