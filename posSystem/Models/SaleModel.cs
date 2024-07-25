using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblSale")]
    public class SaleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int saleId { get; set; }
        public int staffId { get; set; }
        public string staffCode { get; set; }
        public string? staffName { get; set; }
        public int? memberId { get; set; }
        public string? memberCode { get; set; }
        public string? memberName { get; set; }
        public int? saleQty { get; set; }
        public int? totalAmount { get; set; }
        public DateTime? saleDate { get; set; }
        public string? paymentMethod { get; set; }
        public string? promotion { get; set; }
        public string? discount { get; set; }
        public string? invoiceNo { get; set; }


        [ForeignKey("staffId")]
        public StaffModel? Staff { get; set; }

        [ForeignKey("memberId")]
        public MemberModel? Member { get; set; }

        public ICollection<SaleDetailModel>? saleDetails { get; set; }
    }

    public class SaleResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public string sortField { get; set; }
        public string sortOrder { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<SaleModel> saleData { get; set; }
    }
}
