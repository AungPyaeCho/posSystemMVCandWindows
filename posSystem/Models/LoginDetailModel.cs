using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace posSystem.Models
{
    [Table("tblLoginDetail")]
    public class LoginDetailModel
    {
        [Key]
        public string? ldId { get; set; }
        public string? adminId { get; set; }
        public string? adminEmail { get; set; }
        public string? sessionId { get; set; }
        public DateTime? sessionExpired { get; set; }
        public string? loginAt { get; set; }
        public string? logOutAt { get; set; }

        [ForeignKey("adminId")]
        public AdminModel Admin { get; set; }
    }

    public class LoginDetailResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<SaleDetailModel> saleDetailData { get; set; }
    }
}
