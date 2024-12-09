using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Please Enter City Name")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Please Enter City Code")]
        public string CityCode { get; set; }
        public int? StateID { get; set; }
        public int? CountryID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
    public class LOC_CityDropDownModel {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public int? CountryID { get; set; }

        public int? StateID { get; set;}
    }
}
