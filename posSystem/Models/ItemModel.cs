using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblItem")]
    public class ItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int itemId { get; set; }
        public string? itemCode { get; set; }
        public string? itemName { get; set; }
        public int? itemCategory { get; set; }
        public int? itemSubCategory { get; set; }
        public string? itemDescription { get; set; }
        public string? itemBarcode { get; set; }
        public int? itemStock { get; set; }
        public int? itemSold { get; set; }
        public int? itemRemainStock { get; set; }
        public string? itemCreateAt { get; set; }
        public string? itemUpdateAt { get; set; }
        public int? itemUpdateCount { get; set; }
        public int? creatorName { get; set; }


        [ForeignKey("catId")]
        public CategoryModel Category { get; set; }

        [ForeignKey("subCId")]
        public SubCategoryModel SubCategory { get; set; }
    }
    public class ItemResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<ItemModel> itemData { get; set; }
    }
}
