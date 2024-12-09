using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication1.Areas.MST_Student.Models;
using WebApplication1.Areas.MST_Branch.Models;
using WebApplication1.Areas.LOC_Country.Models;
using WebApplication1.Areas.LOC_State.Models;
using WebApplication1.Areas.LOC_City.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Operations;
using System.Net;

namespace WebApplication1.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/[Controller]/[Action]")]
    public class MST_StudentController : Controller
    {

        #region Configuration

        private IConfiguration Configuration;
        public MST_StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Read
        public IActionResult Index()
        {
            CountryDropDown();
            DropDownState();
            DropDownCity();
            BranchDropDown();
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Student_SelectAll";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MST_StudentList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            CountryDropDown();
            DropDownState();
            DropDownCity();
            BranchDropDown();
            return View("MST_StudentAddEdit");
        }
        #region Edit
        public IActionResult Edit(int StudentID)
        {
            CountryDropDown();
            DropDownState();
            DropDownCity();
            BranchDropDown();

            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Student_SelectByPK";
            objcmd.Parameters.AddWithValue("StudentID", StudentID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            MST_StudentModel Cmodel = new MST_StudentModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.StudentID = int.Parse(dr["StudentID"].ToString());
                Cmodel.BranchID = int.Parse(dr["BranchID"].ToString());
                Cmodel.CityID = int.Parse(dr["CityID"].ToString());
                Cmodel.StudentName = dr["StudentName"].ToString();
                Cmodel.MobileNoStudent = dr["MobileNoStudent"].ToString();
                Cmodel.MobileNoFather = dr["MobileNoFather"].ToString();
                Cmodel.IsActive = Convert.ToBoolean(dr["IsActive"]);
                Cmodel.Age = int.Parse(dr["Age"].ToString());
                Cmodel.Email = dr["Email"].ToString();
                Cmodel.Address = dr["Address"].ToString();
                Cmodel.Gender = dr["Gender"].ToString();
                Cmodel.Password = dr["Password"].ToString();
                Cmodel.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

            }

            return View("MST_StudentAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(MST_StudentModel modelMST_Student)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelMST_Student.StudentID == null)
            {
                objcmd.CommandText = "PR_MST_Student_Insert";

            }
            else
            {
                objcmd.CommandText = "PR_MST_Student_UpdateByPK";
                objcmd.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = modelMST_Student.StudentID;
            }
            objcmd.Parameters.AddWithValue("@StudentName", modelMST_Student.StudentName);
            objcmd.Parameters.AddWithValue("@BranchID", modelMST_Student.BranchID);
            objcmd.Parameters.AddWithValue("@MobileNoFather", modelMST_Student.MobileNoFather);
            objcmd.Parameters.AddWithValue("@MobileNoStudent", modelMST_Student.MobileNoStudent);
            objcmd.Parameters.AddWithValue("@Email", modelMST_Student.Email);
            objcmd.Parameters.AddWithValue("@Address", modelMST_Student.Address);
            objcmd.Parameters.AddWithValue("@Age", (modelMST_Student.Age == 0 ? null : modelMST_Student.Age));
            objcmd.Parameters.AddWithValue("@Gender", modelMST_Student.Gender);
            objcmd.Parameters.AddWithValue("@CityID", modelMST_Student.CityID); 
            objcmd.Parameters.AddWithValue("@BirthDate", modelMST_Student.BirthDate);
            objcmd.Parameters.AddWithValue("@IsActive", modelMST_Student.IsActive);
            objcmd.Parameters.AddWithValue("@Password", modelMST_Student.Password);



            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StudentID)
        {
            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_MST_Student_DeleteByPK";
            objcmd.Parameters.AddWithValue("@StudentID", StudentID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Search
        public IActionResult Search(string StudentName, string Email, string Address, int Age, string Gender, int? CityID = null, int? BranchID = null, int? StateID = null,int? CountryID = null)
        {

            CountryDropDown();
            DropDownState();
            DropDownCity();
            BranchDropDown();

            ViewBag.StudentName = StudentName;
            ViewBag.Email = Email;
            ViewBag.Address = Address;
            ViewBag.Age = Age;
            ViewBag.Gender = Gender;


            string connectionstr = Configuration.GetConnectionString("MyConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SearchByStudent";
            objcmd.Parameters.AddWithValue("StudentName", StudentName);
            objcmd.Parameters.AddWithValue("BranchID", BranchID);
            objcmd.Parameters.AddWithValue("Email", Email);
            objcmd.Parameters.AddWithValue("Address", Address);
            objcmd.Parameters.AddWithValue("Age", (Age == 0 ? null : Age));
            objcmd.Parameters.AddWithValue("Gender", Gender);
            objcmd.Parameters.AddWithValue("CityID", CityID);
            objcmd.Parameters.AddWithValue("StateID", StateID);
            objcmd.Parameters.AddWithValue("CountryID", CountryID);

            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MST_StudentList", dt);
        }
        #endregion Search

        #region DropDown's


        #region Branch_dd
        public void BranchDropDown()
        {

            string connectionStr = Configuration.GetConnectionString("MyConnectionString");
            List<MST_BranchDropDownModel> Branchlist = new List<MST_BranchDropDownModel>();
            SqlConnection sconn = new SqlConnection(connectionStr);
            sconn.Open();
            SqlCommand scmd = sconn.CreateCommand();
            scmd.CommandType = CommandType.StoredProcedure;
            scmd.CommandText = "PR_Branch_DropDown";
            SqlDataReader sobj = scmd.ExecuteReader();
            if (sobj.HasRows)
            {
                while (sobj.Read())
                {
                    MST_BranchDropDownModel sModel = new MST_BranchDropDownModel();
                    sModel.BranchID = Convert.ToInt32(sobj["BranchID"]);
                    sModel.BranchName = sobj["BranchName"].ToString();
                    Branchlist.Add(sModel);
                }
                ViewBag.BranchList = Branchlist;
            }
            sconn.Close();
        }
        #endregion

        #region Country_dd
        public void CountryDropDown()
        {
            List<LOC_CountryDropDownModel> Countrylist = new List<LOC_CountryDropDownModel>();
            string connectionStr = Configuration.GetConnectionString("MyConnectionString");
            SqlConnection conn = new SqlConnection(connectionStr);
            conn.Open();
            SqlCommand ddcmd = conn.CreateCommand();
            ddcmd.CommandType = CommandType.StoredProcedure;
            ddcmd.CommandText = "PR_Country_DropDown";
            SqlDataReader obj = ddcmd.ExecuteReader();
            if (obj.HasRows)
            {
                while (obj.Read())
                {
                    LOC_CountryDropDownModel cModel = new LOC_CountryDropDownModel();
                    cModel.CountryID = Convert.ToInt32(obj["CountryID"]);
                    cModel.CountryName = obj["CountryName"].ToString();
                    Countrylist.Add(cModel);
                }
                ViewBag.CountryList = Countrylist;
            }
            conn.Close();
        }
        #endregion
        public void DropDownState(int? CountryID = null)
        {
            ViewBag.StateList = StateDropDown(CountryID);
        }
        public void DropDownCity(int? StateID = null)
        {
            ViewBag.CityList = CityDropDown(StateID);
        }
        public IActionResult DropDownCountry(int CountryID)
        {
            return Json(StateDropDown(CountryID));
        }
        #region State_dd
        public IActionResult DropDownByState(int StateID)
        {
            return Json(CityDropDown(StateID));
        }

        public List<LOC_StateDropDownModel> StateDropDown(int? CountryID = null)
        {   
            string connectionStr = Configuration.GetConnectionString("MyConnectionString");
            List<LOC_StateDropDownModel> Statelist = new List<LOC_StateDropDownModel>();
            SqlConnection sconn = new SqlConnection(connectionStr);
            sconn.Open();
            SqlCommand scmd = sconn.CreateCommand();
            scmd.CommandType = CommandType.StoredProcedure;
            scmd.CommandText = "PR_State_DropDown";
            scmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader sobj = scmd.ExecuteReader();

            if (sobj.HasRows)
            {
                while (sobj.Read())
                {
                    LOC_StateDropDownModel sModel = new LOC_StateDropDownModel();
                    sModel.StateID = Convert.ToInt32(sobj["StateID"]);
                    sModel.StateName = sobj["StateName"].ToString();
                    sModel.CountryID = Convert.ToInt32(sobj["CountryID"]);
                    Statelist.Add(sModel);
                }

            }
            sconn.Close();

            return Statelist;
        }
        #endregion

        #region City_dd
        public List<LOC_CityDropDownModel> CityDropDown(int? StateID = null)
        {
            string connectionStr = Configuration.GetConnectionString("MyConnectionString");
            List<LOC_CityDropDownModel> Citylist = new List<LOC_CityDropDownModel>();
            SqlConnection sconn = new SqlConnection(connectionStr);
            sconn.Open();
            SqlCommand scmd = sconn.CreateCommand();
            scmd.CommandType = CommandType.StoredProcedure;
            scmd.CommandText = "PR_City_DropDown";
            scmd.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader sobj = scmd.ExecuteReader();

            if (sobj.HasRows)
            {
                while (sobj.Read())
                {
                    LOC_CityDropDownModel sModel = new LOC_CityDropDownModel();
                    sModel.CityID = Convert.ToInt32(sobj["CityID"]);
                    sModel.CityName = sobj["CityName"].ToString();
                    Citylist.Add(sModel);
                }

            }
            sconn.Close();

            return Citylist;
        }
        #endregion

        #endregion
    }
}