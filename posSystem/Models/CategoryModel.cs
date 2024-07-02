using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblCategory")]
    public class CategoryModel
    {
        public int catId { get; set; }
        public string catName { get; set; }
        public string catDescription { get; set; }
        public string catCode { get; set; }
        public DateTime? catCreatedAt { get; set; }

        public ICollection<SubCategoryModel> subCategories { get; set; }
        public ICollection<ItemModel> items { get; set; }
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
