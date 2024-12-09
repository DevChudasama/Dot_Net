using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Admin.Models
{
    public class CategoryModel
    {
        [Required]
        public int? CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }

    public class CategoryDropDownModel
    {
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
