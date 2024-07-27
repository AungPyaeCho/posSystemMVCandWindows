using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class DiscountModel
    {
        public int disId { get; set; }
        public string? disName { get; set; }
        public string? disDescription { get; set; }
        public string? disCreateAt { get; set; }
        public string? disUpdateAt { get; set; }
        public int? disUpdateCount { get; set; }
    }
}
