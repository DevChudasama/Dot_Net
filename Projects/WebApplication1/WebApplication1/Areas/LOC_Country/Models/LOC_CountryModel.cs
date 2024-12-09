﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "Please Enter Country Name")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Please Enter Country Code")]
        public string CountryCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
    public class LOC_CountryDropDownModel {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
