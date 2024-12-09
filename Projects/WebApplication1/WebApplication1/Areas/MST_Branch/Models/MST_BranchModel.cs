using Microsoft.CodeAnalysis.Operations;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.MST_Branch.Models
{
    public class MST_BranchModel
    {

        public int? BranchID { get; set; }
        [Required(ErrorMessage = "Please Enter Branch Name")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Please Enter Branch Code")]
        public string BranchCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
    public class MST_BranchDropDownModel
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
    }
}
