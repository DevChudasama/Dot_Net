using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Areas.LOC_City.Models;
using WebApplication1.Areas.LOC_Country.Models;
using WebApplication1.Areas.LOC_State.Models;

namespace WebApplication1.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[Controller]/[Action]")]
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Read
        public IActionResult Index()
        {
            CountryDropDown();
            StateDropdown();
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_City_SelectAll";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_CityList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            CountryDropDown();
            StateDropdown();
            return View("LOC_CityAddEdit");
        }
        #region Edit
        public IActionResult Edit(int CityID)
        {
            CountryDropDown();
            StateDropdown();
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_City_SelectByPK";
            objcmd.Parameters.AddWithValue("CityID", CityID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            LOC_CityModel Cmodel = new LOC_CityModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.CityID = int.Parse(dr["CityID"].ToString());
                Cmodel.StateID = int.Parse(dr["StateID"].ToString());
                Cmodel.CountryID = int.Parse(dr["CountryID"].ToString());
                Cmodel.CityName = dr["CityName"].ToString();
                Cmodel.CityCode = dr["CityCode"].ToString();
            }

            return View("LOC_CityAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_City.CityID == null)
            {
                objcmd.CommandText = "PR_City_Insert";

            }
            else
            {
                objcmd.CommandText = "PR_City_UpdateByPK";
                objcmd.Parameters.Add("@CityID", SqlDbType.VarChar).Value = modelLOC_City.CityID;

            }
            objcmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = modelLOC_City.StateID;
            objcmd.Parameters.Add("@CountryID", SqlDbType.VarChar).Value = modelLOC_City.CountryID;
            objcmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = modelLOC_City.CityName;
            objcmd.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = modelLOC_City.CityCode;
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_City_DeleteByPK";
            objcmd.Parameters.AddWithValue("@CityID", CityID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Search
        public IActionResult Search( string CityName, string CityCode,int? CountryID = null, int? StateID = null)
        {
            CountryDropDown();
            StateDropdown();
          
            ViewBag.CityName = CityName;
            ViewBag.CityCode = CityCode;
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SearchByCity";
            objcmd.Parameters.AddWithValue("@CountryID", CountryID);
            objcmd.Parameters.AddWithValue("@StateID", StateID);
            objcmd.Parameters.AddWithValue("@CityName", CityName);
            objcmd.Parameters.AddWithValue("@CityCode", CityCode);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_CityList", dt);
        }
        #endregion Search

        #region CountryDropdown 
        public IActionResult DropDownCountry(int? CountryID)
        {
            return Json(StateDropdown(CountryID));
        }
        public void CountryDropDown()
        {
            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Country_DropDown";
            SqlDataReader objsdr = objcmd.ExecuteReader();

            if (objsdr.HasRows)
            {
                while (objsdr.Read())
                {
                    LOC_CountryDropDownModel model = new LOC_CountryDropDownModel();
                    model.CountryID = Convert.ToInt32(objsdr["CountryID"]);
                    model.CountryName = objsdr["CountryName"].ToString();
                    list.Add(model);
                }
                ViewBag.CountryList = list;
            }
            conn.Close();

        }
        #endregion

        #region StateDropdown
        public void DropDownState(int? CountryID = null)
        {
            ViewBag.StateList = StateDropdown(CountryID);
        }
        public List<LOC_StateDropDownModel> StateDropdown(int? CountryID = null)
        {
            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_DropDown";
            objcmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objsdr = objcmd.ExecuteReader();

            if (objsdr.HasRows)
            {
                while (objsdr.Read())
                {
                    LOC_StateDropDownModel model = new LOC_StateDropDownModel();
                    model.StateID = Convert.ToInt32(objsdr["StateID"]);
                    model.StateName = objsdr["StateName"].ToString();
                    model.CountryID = Convert.ToInt32(objsdr["CountryID"]);
                    list.Add(model);
                }
                ViewBag.StateList = list;
            }
            conn.Close();
            return(list);
        }
        #endregion
    }
}
