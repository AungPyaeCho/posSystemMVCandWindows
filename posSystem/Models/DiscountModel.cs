using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblDiscount")]
    public class DiscountModel
    {
        [Key]
        public int disId { get; set; }
        public string? disName { get; set; }
        public string? disDescription { get; set; }
    }
}
