using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblSaleDetail")]
    public class SaleDetailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sdId { get; set; }
        public int? saleId { get; set; }
        public int? itemId { get; set; }
        public int? saleQuantity { get; set; }
        public int? totalQty { get; set; }
        public int? itemPrice { get; set; }
        public int? totalAmount { get; set; }
        public string? saleDate { get; set; }

        [ForeignKey("saleId")]
        public SaleModel Sale { get; set; }

        [ForeignKey("itemId")]
        public ItemModel Item { get; set; }
    }

    public class SaleDetailResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<SaleDetailModel> saleDetailData { get; set; }
    }

}
