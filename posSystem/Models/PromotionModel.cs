using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblPromotion")]
    public class PromotionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int proId { get; set; }
        public string? proName { get; set; }
        public string? proCode { get; set; }
        public string? proDescription { get; set; }
        public string? proCreateAt { get; set; }
        public string? proUpdateAt { get; set; }
        public int? proUpdateCount { get; set; }
    }

    public class PromotionResponseModel
    {
        public List<PromotionModel> promotionData { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public int pageNo { get; set; }
        public string sortField { get; set; }
        public string sortOrder { get; set; }
    }
}
