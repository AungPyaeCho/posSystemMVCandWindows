using Microsoft.AspNetCore.Mvc;

namespace posSystem.Models
{
    public class UploadViewModel
    {
        public IFormFile? File { get; set; }
    }
}
