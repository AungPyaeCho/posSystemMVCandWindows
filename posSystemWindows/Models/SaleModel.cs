using posSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class SaleModel
    {
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
    }
}
