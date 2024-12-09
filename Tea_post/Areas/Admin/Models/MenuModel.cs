using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Admin.Models
{
    public class MenuModel
    {
        [Required]
        public int? MenuID { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        [Required]
        public string MenuImage { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile File { get; set; }

    }
}
