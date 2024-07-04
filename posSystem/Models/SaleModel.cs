using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblSale")]
    public class SaleModel
    {
        [Key]
        public int saleId { get; set; }
        public string? staffId { get; set; }
        public string? memberId { get; set; }
        public int? saleQty { get; set; }
        public int? totalAmount { get; set; }
        public DateTime? saleDate { get; set; }
        public string? paymentMethod { get; set; }
        public string? promotion { get; set; }
        public string? discount { get; set; }

        public StaffModel staff { get; set; }
        public MemberModel member { get; set; }
        public ICollection<SaleDetailModel> saleDetails { get; set; }
    }

    public class SaleResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<SaleModel> saleData { get; set; }
    }
}
