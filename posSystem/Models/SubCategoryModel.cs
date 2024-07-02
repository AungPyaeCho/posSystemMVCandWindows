using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblSubCategory")]
    public class SubCategoryModel
    {
        public int subCId { get; set; }
        public string subCatName { get; set; }
        public string subCatCode { get; set; }
        public DateTime? subCatCreatedAt { get; set; }

        public string catCode { get; set; }
        public CategoryModel category { get; set; }
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
