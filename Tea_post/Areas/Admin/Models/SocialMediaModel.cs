using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Admin.Models
{
    public class SocialMediaModel
    {
        [Required]
        public int? SocialMediaID { get; set; }

        [Required]
        public string SocialMediaImage { get; set; } = string.Empty;

        [Required]
        public string SocialMediaLink { get; set; } = string.Empty;

        public IFormFile File { get; set; }

    }
}
