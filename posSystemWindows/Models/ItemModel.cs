using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class ItemModel
    {
        public int itemId { get; set; }
        public string? itemCode { get; set; }
        public string? itemName { get; set; }
        public int? itemBuyPrice { get; set; }
        public int? itemSalePrice { get; set; }
        public int? itemWholeSalePrice { get; set; }
        public int? catId { get; set; }
        public string? catCode { get; set; }
        public string? itemCategory { get; set; }
        public int? subCId { get; set; }
        public string? subCatCode { get; set; }
        public string? itemSubCategory { get; set; }
        public int? brandId { get; set; }
        public string? brandCode { get; set; }
        public string? itemBrand { get; set; }
        public int? subBId { get; set; }
        public string? subBrandCode { get; set; }
        public string? itemSubBrand { get; set; }
        public string? itemColor { get; set; }
        public string? itemBarcode { get; set; }
        public int? itemStock { get; set; }
        public int? itemSold { get; set; }
        public int? itemRemainStock { get; set; }
        public string? itemDescription { get; set; }
        public string? itemCreateAt { get; set; }
        public string? itemUpdateAt { get; set; }
        public int? itemUpdateCount { get; set; }
        public string? creatorName { get; set; }

        [ForeignKey("brandId")]
        public BrandModel Brand { get; set; }

        [ForeignKey("subBId")]
        public SubBrandModel SubBrand { get; set; }

        [ForeignKey("catId")]
        public CategoryModel Category { get; set; }

        [ForeignKey("subCId")]
        public SubCategoryModel SubCategory { get; set; }
    }
}
