using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class PromotionModel
    {
        public int proId { get; set; }
        public string? proName { get; set; }
        public string? proCode { get; set; }
        public string? proDescription { get; set; }
        public string? proCreateAt { get; set; }
        public string? proUpdateAt { get; set; }
        public int? proUpdateCount { get; set; }
    }
}
