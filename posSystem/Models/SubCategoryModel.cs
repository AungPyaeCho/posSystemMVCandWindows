using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblSubCategory")]
    public class SubCategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subCId { get; set; }
        public string? subCatName { get; set; }
        public string? subCatCode { get; set; }
        public string? subCatCreateAt { get; set; }
        public string? subCatUpdateAt { get; set; }
        public int? subCatUpdateCount { get; set; }
        public int? catId {  get; set; }
        public string? catCode { get; set; }
        public string? catName { get; set; }

        [ForeignKey("catId")]
        public CategoryModel Category { get; set; }
    }

    public class SubCategoryResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<SubCategoryModel> subCategoryData { get; set; }
    }
}
