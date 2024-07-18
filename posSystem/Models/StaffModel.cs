using Microsoft.AspNetCore.Mvc;
using posSystem.Middlewares;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace posSystem.Models
{
    [Table("tblStaff")]
    public class StaffModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int staffId { get; set; }
        public string? staffCode { get; set; }
        public string? staffName { get; set; }
        public string? staffEmail { get; set; }
        public string? staffPassword { get; set; }
        public string? staffPhone { get; set; }
        public string? staffAddress { get; set; }
        public string? staffRole { get; set; }
        public string? staffPhoto { get; set; }
        public DateTime? staffJoinedDate { get; set; }
        public string? staffCreateAt { get; set; }
        public string? staffUpdateAt { get; set; }
        public int? staffUpdateCount { get; set; }

        public void SetEncryptedPassword(string plainPassword)
        {
            this.staffPassword = SimpleEncryptionHelper.Encrypt(plainPassword);
        }

        public string GetDecryptedPassword()
        {
            return SimpleEncryptionHelper.Decrypt(this.staffPassword)!;
        }
    }

    public class StaffResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<StaffModel> staffData { get; set; }
    }
}
