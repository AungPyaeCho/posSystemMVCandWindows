using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblBrand")]
    public class BrandModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brandId { get; set; }
        public string? brandName { get; set; }
        public string? brandDescription { get; set; }
        public string? brandCode { get; set; }
        public string? brandCreatedAt { get; set; }
        public string? brandUpdatedAt { get; set; }
        public string? brandDeleteAt { get; set; }
        public int? brandUpdateCount { get; set; }
    }

    public class BrandResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public string sortField { get; set; }
        public string sortOrder { get; set; }

        public bool? isEndofpage => pageNo >= pageCount;
        public List<BrandModel> brandData { get; set; }
    }
}
