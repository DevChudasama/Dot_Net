using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Account.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string IsAdmin { get; set; }
    }
}
