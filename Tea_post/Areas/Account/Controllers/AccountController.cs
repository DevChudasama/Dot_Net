using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;
using Tea_post.Areas.Account.Models;
using System.Runtime.Intrinsics.Arm;


namespace Tea_post.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/[controller]/[Action]")]
    public class AccountController : Controller
    {

        private readonly IConfiguration ConnectionString;

        public AccountController(IConfiguration _configuration)
        {
            ConnectionString = _configuration;

        }
        public IActionResult Index()
        
        {
            return View();
        }

        public IActionResult Register()

        {
            return View();
        }

        #region Login
        public IActionResult Login(AccountModel accountModel)
        {
            string connectionStr = ConnectionString.GetConnectionString("MyStr");
            SqlConnection conn1 = new SqlConnection(connectionStr);
            conn1.Open();
            SqlCommand cmd = conn1.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Login_Check";
            cmd.Parameters.AddWithValue("@Username", accountModel.UserName);
            cmd.Parameters.AddWithValue("@Password", accountModel.Password);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    HttpContext.Session.SetInt32("UserID", Convert.ToInt32(rdr["UserID"]));
                    HttpContext.Session.SetString("UserName", rdr["UserName"].ToString());
                    HttpContext.Session.SetString("Phone", rdr["Contact"].ToString());
                    HttpContext.Session.SetString("Email", rdr["Email"].ToString());
                    HttpContext.Session.SetInt32("IsActive", Convert.ToInt32(rdr["IsActive"]));
                    HttpContext.Session.SetInt32("IsAdmin", Convert.ToInt32(rdr["IsAdmin"]));
                }
                conn1.Close();

                if (HttpContext.Session.GetInt32("UserID") != null && HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetInt32("IsAdmin") != null)
                {
                    if (HttpContext.Session.GetInt32("IsAdmin") == 1)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Website", new { area = "Website" });
                    }
                }
                else
                {
                    ViewData["ErrorMsg"] = "Invalid Username Or Password";


            return View();
        }


            }
            else
            {
                ViewData["ErrorMsg"] = "Invalid Username Or Password";

                return View();
        
    }

        }
        #endregion

        #region Register
        public IActionResult RegisterSave(RegisterModel accountModel)
        {
            

            string connectionStr = ConnectionString.GetConnectionString("MyStr");
            SqlConnection conn1 = new SqlConnection(connectionStr);
            conn1.Open();
            SqlCommand cmd = conn1.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_INSERT_User";
            cmd.Parameters.AddWithValue("@UserName", accountModel.UserName);
            cmd.Parameters.AddWithValue("@Contact", accountModel.Contact);
            cmd.Parameters.AddWithValue("@Email", accountModel.Email);
            cmd.Parameters.AddWithValue("@Password", accountModel.Password);
            SqlDataReader rdr = cmd.ExecuteReader();
            TempData["Message"] = "You Are Register Successfully Please Login";
            return RedirectToAction("Index");

        }
        #endregion

        #region Logout
        public IActionResult Logout()

        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Account",new { area="Account" });
        }
        #endregion
    }
}


//if (rdr.HasRows)
//{
//    while (rdr.Read())
//    {
//        HttpContext.Session.SetInt32("UserID", Convert.ToInt32(rdr["UserID"]));
//        HttpContext.Session.SetString("UserName", rdr["UserName"].ToString());
//        HttpContext.Session.SetString("Phone", rdr["Contact"].ToString());
//        HttpContext.Session.SetString("Email", rdr["Email"].ToString());
//        HttpContext.Session.SetInt32("IsActive", Convert.ToInt32(rdr["IsActive"]));
//        HttpContext.Session.SetInt32("IsAdmin", Convert.ToInt32(rdr["IsAdmin"]));
//    }
//    conn1.Close();

//    if (HttpContext.Session.GetInt32("UserID") != null && HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetInt32("IsAdmin") != null)
//    {
//        if (HttpContext.Session.GetInt32("IsAdmin") == 1)
//        {
//            return RedirectToAction("Index", "Admin", new { area = "Admin" });
//        }
//        else
//        {
//            return RedirectToAction("Index", "Dashboard", new { area = "Website" });
//        }
//    }
//    else
//    {
//        ViewData["ErrorMsg"] = "Invalid Username Or Password";


//        return View();
//    }


//}
//else
//{
//    ViewData["ErrorMsg"] = "Invalid Username Or Password";

//    return View();

//}