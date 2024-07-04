using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblItem")]
    public class ItemModel
    {
        [Key]
        public int itemId { get; set; }
        public string? itemCode { get; set; }
        public string? itemName { get; set; }
        public string? itemCategory { get; set; }
        public string? itemSubCategory { get; set; }
        public string? itemDescription { get; set; }
        public string? itemBarcode { get; set; }
        public int? itemStock { get; set; }
        public int? itemSold { get; set; }
        public int? itemRemainStock { get; set; }
        public DateTime? itemCreatedAt { get; set; }

        public CategoryModel category { get; set; }
        public SubCategoryModel subCategory { get; set; }
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
