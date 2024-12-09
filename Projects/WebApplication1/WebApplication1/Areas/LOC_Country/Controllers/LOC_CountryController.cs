using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Areas.LOC_Country.Models;

namespace WebApplication1.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[Controller]/[Action]")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
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
            objcmd.CommandText = "PR_Country_SelectAll";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_CountryList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            return View("LOC_CountryAddEdit");
        }

        #region Edit
        public IActionResult Edit(int CountryID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Country_SelectByPK";
            objcmd.Parameters.AddWithValue("CountryID", CountryID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            LOC_CountryModel Cmodel = new LOC_CountryModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.CountryID = int.Parse(dr["CountryID"].ToString());
                Cmodel.CountryName = dr["CountryName"].ToString();
                Cmodel.CountryCode = dr["CountryCode"].ToString();
            }

            return View("LOC_CountryAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_Country.CountryID == null)
            {
                objcmd.CommandText = "PR_Country_Insert";

            }
            else
            {
                objcmd.CommandText = "PR_Country_UpdateByPK";
                objcmd.Parameters.Add("@CountryID", SqlDbType.VarChar).Value = modelLOC_Country.CountryID;
            }
            objcmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            objcmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;

            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Country_DeleteByPK";
            objcmd.Parameters.AddWithValue("@CountryID", CountryID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Search
        public IActionResult Search(string CountryName/*,string CountryCode*/) 
        {
            ViewBag.CountryName = CountryName;
            //ViewBag.CountryCode = CountryCode;
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SearchByCountry";
            objcmd.Parameters.AddWithValue("CountryName", CountryName);
            //objcmd.Parameters.AddWithValue("CountryCode", CountryCode);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_CountryList", dt);
        }
        #endregion Search
    }
}
