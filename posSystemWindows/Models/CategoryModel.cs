using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class CategoryModel
    {
        public int catId { get; set; }
        public string? catName { get; set; }
        public string? catDescription { get; set; }
        public string? catCode { get; set; }
        public string? catCreatedAt { get; set; }
        public string? catUpdatedAt { get; set; }
        public int? catUpdateCount { get; set; }
        public string? catDeleteAt { get; set; }

    }
}
