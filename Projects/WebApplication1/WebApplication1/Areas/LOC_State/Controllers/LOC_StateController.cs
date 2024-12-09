using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using WebApplication1.Areas.LOC_Country.Models;
using WebApplication1.Areas.LOC_State.Models;

namespace WebApplication1.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[Controller]/[Action]")]
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Read
        public IActionResult Index()
        {
            CountryDropDown();

            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_SelectAll";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_StateList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            CountryDropDown();
            return View("LOC_StateAddEdit");
        }
        #region Edit
        public IActionResult Edit(int StateID)
        {
            CountryDropDown();

            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_SelectByPK";
            objcmd.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            LOC_StateModel Cmodel = new LOC_StateModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.StateID = int.Parse(dr["StateID"].ToString());
                Cmodel.StateName = dr["StateName"].ToString();
                Cmodel.StateCode = dr["StateCode"].ToString();
                Cmodel.CountryID = int.Parse(dr["CountryID"].ToString());

            }

            return View("LOC_StateAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_State.StateID == null)
            {
                objcmd.CommandText = "PR_State_Insert";

            }
            else
            {
                objcmd.CommandText = "PR_State_UpdateByPK";
                objcmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = modelLOC_State.StateID;

            }
            objcmd.Parameters.Add("@CountryID", SqlDbType.VarChar).Value = modelLOC_State.CountryID;
            objcmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = modelLOC_State.StateName;
            objcmd.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = modelLOC_State.StateCode;



            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_State_DeleteByPK";
            objcmd.Parameters.AddWithValue("@StateID", StateID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Search
        public IActionResult Search(int? CountryID,string CountryName, string StateName, string StateCode)
        {
            CountryDropDown();
            ViewBag.CountryName = CountryName;
            ViewBag.StateName = StateName;
            ViewBag.StateCode = StateCode;
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SearchByState";
            objcmd.Parameters.AddWithValue("CountryID", CountryID);
            //objcmd.Parameters.AddWithValue("CountryName", CountryName);
            objcmd.Parameters.AddWithValue("StateName", StateName);
            objcmd.Parameters.AddWithValue("StateCode", StateCode);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("LOC_StateList", dt);
        }
        #endregion Search

        #region CountryDropdown
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
                while(objsdr.Read())
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
         
    }
}
