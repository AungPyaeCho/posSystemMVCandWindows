﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace posSystem.Models
{
    [Table("tblLoginStaffDetail")]
    public class LoginStaffDetailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ldId { get; set; }
        public int? staffId { get; set; }
        public string? staffName { get; set; }
        public string? staffCode { get; set; }
        public string? staffRole {  get; set; }
        public string? sessionId { get; set; }
        public DateTime? sessionExpired { get; set; }
        public DateTime? loginAt { get; set; }
        public DateTime? logOutAt { get; set; }


        [ForeignKey("staffId")]
        public StaffModel Staff { get; set; }
    }

    public class LoginStaffDetailResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<LoginStaffDetailModel> loginStaffData { get; set; }
    }
}
