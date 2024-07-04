using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblDiscount")]
    public class DiscountModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int disId { get; set; }
        public string? disName { get; set; }
        public string? disDescription { get; set; }
        public string? disCreateAt { get; set; }
        public string? disUpdateAt { get; set; }
        public int? disUpdateCount { get; set; }
    }
}
