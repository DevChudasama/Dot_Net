using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Areas.MST_Branch.Models;

namespace WebApplication1.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[Controller]/[Action]")]
    public class MST_BranchController : Controller
    {
        private IConfiguration Configuration;
        public MST_BranchController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Read
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Branch_SelectAll";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MST_BranchList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            return View("MST_BranchAddEdit");
        }
        #region Edit
        public IActionResult Edit(int BranchID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Branch_SelectByPK";
            objcmd.Parameters.AddWithValue("BranchID", BranchID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            MST_BranchModel Cmodel = new MST_BranchModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.BranchID = int.Parse(dr["BranchID"].ToString());
                Cmodel.BranchName = dr["BranchName"].ToString();
                Cmodel.BranchCode = dr["BranchCode"].ToString();

            }

            return View("MST_BranchAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(MST_BranchModel modelMST_Branch)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelMST_Branch.BranchID == null)
            {
                objcmd.CommandText = "PR_MST_Branch_Insert";

            }
            else
            {
                objcmd.CommandText = "PR_MST_Branch_UpdateByPK";
                objcmd.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = modelMST_Branch.BranchID;

            }
            
            objcmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = modelMST_Branch.BranchName;
            objcmd.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = modelMST_Branch.BranchCode;



            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int BranchID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Branch_DeleteByPK";
            objcmd.Parameters.AddWithValue("@BranchID", BranchID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Search
        public IActionResult Search(string BranchName, string BranchCode)
        {
            ViewBag.BranchName = BranchName;
            ViewBag.BranchCode = BranchCode;
          
          

            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SearchByBranch";
            objcmd.Parameters.AddWithValue("BranchName", BranchName);
            objcmd.Parameters.AddWithValue("BranchCode", BranchCode);


            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MST_BranchList", dt);
        }
        #endregion Search
    }
}
