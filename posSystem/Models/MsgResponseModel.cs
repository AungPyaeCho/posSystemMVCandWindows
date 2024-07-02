using Microsoft.AspNetCore.Mvc;

namespace posSystem.Models
{
    public class MsgResopnseModel
    {
        public bool IsSuccess { get; set; }
        public string responeMessage { get; set; }
    }
}
