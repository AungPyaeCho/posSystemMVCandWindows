using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class SubCategoryModel
    {
        public int subCId { get; set; }
        public string? subCatName { get; set; }
        public string? subCatCode { get; set; }
        public int? catId { get; set; }
        public string? catCode { get; set; }
        public string? catName { get; set; }
        public string? subCatCreateAt { get; set; }
        public string? subCatUpdateAt { get; set; }
        public int? subCatUpdateCount { get; set; }
    }
}
