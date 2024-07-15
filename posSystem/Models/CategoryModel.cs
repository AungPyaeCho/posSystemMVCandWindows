using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblCategory")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int catId { get; set; }
        public string? catName { get; set; }
        public string? catDescription { get; set; }
        public string? catCode { get; set; }
        public string? catCreatedAt { get; set; }
        public string? catUpdatedAt { get; set; }
        public int? catUpdateCount { get; set; }
        public string? catDeleteAt { get; set; }
        
    }

    public class CategoryResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<CategoryModel> categoryData { get; set; }
    }
}
