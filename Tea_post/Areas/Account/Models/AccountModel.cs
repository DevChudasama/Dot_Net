using System.ComponentModel.DataAnnotations;

namespace Tea_post.Areas.Account.Models
{
    public class AccountModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
    public class MST_UserModel
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
    }
}
