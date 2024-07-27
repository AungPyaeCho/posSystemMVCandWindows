using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace posSystem.Models
{
    public class MemberModel
    {
        public int memberId { get; set; }
        public string? memberCode { get; set; }
        public string? memberName { get; set; }
        public string? memberEmail { get; set; }
        public string memberPassword { get; set; }
        public string? memberPhone { get; set; }
        public string? memberAddress { get; set; }
        public string? memberPhoto { get; set; }
        public string? memberLevel { get; set; }
        public int? memberPoints { get; set; }
        public int memberUsedPoints { get; set; }
        public string? memberCreateAt { get; set; }
        public string? memberUpdateAt { get; set; }
        public int? memberUpdateCount { get; set; }
    }
}
