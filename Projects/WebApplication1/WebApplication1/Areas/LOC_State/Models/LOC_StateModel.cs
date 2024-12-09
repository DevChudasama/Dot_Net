using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        
        [Required(ErrorMessage = "Please Enter State Name")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Please Enter State Code")]
        public string StateCode { get; set; }
        public int? CountryID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
    }
}
