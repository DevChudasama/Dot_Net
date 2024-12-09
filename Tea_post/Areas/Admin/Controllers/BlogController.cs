using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Tea_post.Areas.Account.Models;
using Tea_post.Areas.Admin.Models;

namespace Tea_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class BlogController : Controller
    {
        #region IConfiguration
        private IConfiguration Configuration;
        public BlogController(IConfiguration _configuration)
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
            objcmd.CommandText = "PR_Read_Blog";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("BlogList", dt);
        }
        #endregion

        //public IActionResult Add()
        //{
        //    UserDropDown();
        //    return View("BlogAddEdit");
        //}

        #region Edit
        public IActionResult AddEdit(int BlogID)
        {
            UserDropDown();

            string connectionstr = Configuration.GetConnectionString("MyStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SelectByPK_Blog";
            objcmd.Parameters.AddWithValue("BlogID", BlogID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            BlogModel Cmodel = new BlogModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.BlogID = int.Parse(dr["BlogID"].ToString());
                Cmodel.UserID = int.Parse(dr["UserID"].ToString());
                Cmodel.BlogImage = dr["BlogImage"].ToString();
                Cmodel.Title = dr["Title"].ToString();
                Cmodel.Content = dr["Content"].ToString();
            }

            return View("BlogAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(BlogModel modelBlog)
        {
            if (modelBlog.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelBlog.File.FileName);
                modelBlog.BlogImage = FilePath.Replace("wwwroot\\", "/") + "/" + modelBlog.File.FileName;

                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelBlog.File.CopyTo(fileStream);
                }
            }
            string connectionstr = Configuration.GetConnectionString("MyStr");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelBlog.BlogID == null)   
            {
                objcmd.CommandText = "PR_Create_Blog";

            }
            else
            {
                objcmd.CommandText = "PR_Update_Blog";
                objcmd.Parameters.Add("@BlogID", SqlDbType.VarChar).Value = modelBlog.BlogID;
            }
            objcmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = modelBlog.UserID;
            objcmd.Parameters.Add("@BlogImage", SqlDbType.VarChar).Value = modelBlog.BlogImage;
            objcmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = modelBlog.Title;
            objcmd.Parameters.Add("@Content", SqlDbType.VarChar).Value = modelBlog.Content;

            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public IActionResult Delete(int BlogID)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Delete_Blog";
            objcmd.Parameters.AddWithValue("@BlogID", BlogID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region UserDropDown
        public void UserDropDown()
        {
            List<MST_UserModel> list = new List<MST_UserModel>();
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_User_DropDown";
            SqlDataReader objsdr = objcmd.ExecuteReader();

            if (objsdr.HasRows)
            {
                while (objsdr.Read())
                {
                    MST_UserModel model = new MST_UserModel();
                    model.UserID = Convert.ToInt32(objsdr["UserID"]);
                    model.UserName = objsdr["UserName"].ToString();
                    list.Add(model);
                }
                ViewBag.UserList = list;
            }
            conn.Close();

        }
        #endregion

        #region FilterByDate
        public IActionResult Search(DateTime startdate, DateTime enddate) 
        {
           
            string connectionstr = Configuration.GetConnectionString("MyStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_filter_ByDate_Blog";
            objcmd.Parameters.AddWithValue("startdate", startdate);
            objcmd.Parameters.AddWithValue("enddate", enddate);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("BlogList", dt);
        }
        #endregion
    }
}
