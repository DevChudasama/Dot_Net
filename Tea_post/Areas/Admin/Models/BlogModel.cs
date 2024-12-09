using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Admin.Models
{
    public class BlogModel
    {
        [Required]
        public int? UserID { get; set; }
        [Required]
        public int? BlogID { get; set; }
        [Required]
        public string BlogImage { get; set;}
        [Required]
        public string Title { get; set;}
        [Required]
        public string Content { get; set;}

        public IFormFile File { get; set; }
    }
}
