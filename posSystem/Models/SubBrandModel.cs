using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace posSystem.Models
{
    [Table("tblSubBrand")]
    public class SubBrandModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subBId { get; set; }
        public string? subBrandName { get; set; }
        public string? subBrandCode { get; set; }
        public int? brandId { get; set; }
        public string? brandCode { get; set; }
        public string? brandName { get; set;}
        public string? subBrandCreateAt { get; set; }
        public string? subBrandUpdateAt { get; set; }
        public int? subBrandUpdateCount { get; set; }
        

        [ForeignKey("brandId")]
        public BrandModel Brand { get; set; }
    }

    public class SubBrandResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public string sortField { get; set; }
        public string sortOrder { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<SubBrandModel> subBrandData { get; set; }
    }
}
