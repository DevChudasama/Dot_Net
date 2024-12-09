using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Tea_post.Areas.Admin.Models;

namespace Tea_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class CategoryController : Controller
    {
        #region IConfiguration
        private IConfiguration Configuration;
        public CategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Read
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Read_Category";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("CategoryList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            return View("CategoryAddEdit");
        }

        #region Edit
        public IActionResult Edit(int CategoryID)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SelectByPK_Category";
            objcmd.Parameters.AddWithValue("CategoryID", CategoryID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            CategoryModel Cmodel = new CategoryModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.CategoryID = int.Parse(dr["CategoryID"].ToString());
                Cmodel.CategoryName = dr["CategoryName"].ToString();
            }

            return View("CategoryAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(CategoryModel modelCategory)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelCategory.CategoryID == null)
            {
                objcmd.CommandText = "PR_Create_Category";

            }
            else
            {
                objcmd.CommandText = "PR_Update_Category";
                objcmd.Parameters.Add("@CategoryID", SqlDbType.VarChar).Value = modelCategory.CategoryID;
            }
            objcmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = modelCategory.CategoryName;

            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CategoryID)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Delete_Category";
            objcmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
