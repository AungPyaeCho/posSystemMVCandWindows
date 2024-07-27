
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystemWindows.Models
{
    public class BrandModel
    {
        public int brandId { get; set; }
        public string? brandName { get; set; }
        public string? brandDescription { get; set; }
        public string? brandCode { get; set; }
        public string? brandCreatedAt { get; set; }
        public string? brandUpdatedAt { get; set; }
        public string? brandDeleteAt { get; set; }
        public int? brandUpdateCount { get; set; }
    }
}
