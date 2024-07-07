using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static posSystem.Controllers.AdminController;

namespace posSystem.Models
{
    [Table("tblAdmin")]
    public class AdminModel
    {
        [Key]
        public string? id { get; set; }
        public string? adminName { get; set; }
        public string? adminEmail { get; set; }
        public string adminPassword { get; set; }
        public string? adminCreateAt { get; set; }
        public string? adminLoginAt{ get; set; }
        public string? adminLoginName { get; set; }

        public void SetEncryptedPassword(string plainPassword)
        {
            this.adminPassword = SimpleEncryptionHelper.Encrypt(plainPassword);
        }

        public string GetDecryptedPassword()
        {
            return SimpleEncryptionHelper.Decrypt(this.adminPassword)!;
        }
    }

    public class loginResponeModel
    {
        public bool IsSuccess { get; set; }
        public string responeMsg { get; set; }
    }

    public class AdminResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<AdminModel> adminData { get; set; }
    }


}
