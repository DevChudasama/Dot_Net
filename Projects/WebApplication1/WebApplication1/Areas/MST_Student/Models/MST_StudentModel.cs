using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.MST_Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }

        [Required(ErrorMessage = "Please Enter Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please Enter Student Mobile Number")]
        public string MobileNoStudent { get; set; }

        [Required(ErrorMessage = "Please Enter Father Mobile Number")]
        public string MobileNoFather { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Age")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Please Enter Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public int CityID{ get; set; }
        public int BranchID{ get; set; }

        [Required(ErrorMessage = "Please Enter Birth Date")]
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
}
