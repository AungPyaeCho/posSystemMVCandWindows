using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblPromotion")]
    public class PromotionModel
    {
        public int proId { get; set; }
        public string proName { get; set; }
        public string proCode { get; set; }
        public string proDescription { get; set; }
    }
}
