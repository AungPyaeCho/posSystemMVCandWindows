using posSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class SaleDetailModel
    {
        public int sdId { get; set; }
        public string? invoiceNo { get; set; }
        public int? saleId { get; set; }
        public int? itemId { get; set; }
        public string itemCode { get; set; }
        public int? saleQuantity { get; set; }
        public int? totalQty { get; set; }
        public int? itemPrice { get; set; }
        public int? totalAmount { get; set; }
        public DateTime saleDate { get; set; }

    }
}
