using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Tea_post.Areas.Admin.Models;

namespace Tea_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class MenuController : Controller
    {
        
        #region IConfiguration
        private IConfiguration Configuration;
        public MenuController(IConfiguration _configuration)
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
            objcmd.CommandText = "PR_Read_Menu";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MenuList", dt);
        }
        #endregion

        //public IActionResult Add()
        //{
        //    CategoryDropDown();
        //    return View("MenuAddEdit");
        //}

        #region Edit
        public IActionResult AddEdit(int MenuID)
        {
            CategoryDropDown();
            string connectionstr = Configuration.GetConnectionString("MyStr");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_SelectByPK_Menu";
            objcmd.Parameters.AddWithValue("@MenuID", MenuID);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);

            MenuModel Cmodel = new MenuModel();

            foreach (DataRow dr in dt.Rows)
            {
                Cmodel.MenuID = int.Parse(dr["MenuID"].ToString());
                Cmodel.CategoryID = int.Parse(dr["CategoryID"].ToString());
                Cmodel.MenuImage = dr["MenuImage"].ToString();
                Cmodel.Name = dr["Name"].ToString();
                Cmodel.Price = double.Parse(dr["Price"].ToString());
                Cmodel.Description = dr["Description"].ToString();
            }

            return View("MenuAddEdit", Cmodel);
        }
        #endregion

        #region Add/Edit
        [HttpPost]
        public IActionResult Save(MenuModel modelMenu)
        {
            if (modelMenu.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelMenu.File.FileName);
                modelMenu.MenuImage = FilePath.Replace("wwwroot\\", "/") + "/" + modelMenu.File.FileName;

                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMenu.File.CopyTo(fileStream);
                }
            }
            string connectionstr = Configuration.GetConnectionString("MyStr");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelMenu.MenuID == null)
            {
                objcmd.CommandText = "PR_Create_Menu";

            }
            else
            {
                objcmd.CommandText = "PR_Update_Menu";
                objcmd.Parameters.Add("@MenuID", SqlDbType.VarChar).Value = modelMenu.MenuID;
            }
            objcmd.Parameters.Add("@CategoryID", SqlDbType.VarChar).Value = modelMenu.CategoryID;
            objcmd.Parameters.Add("@MenuImage", SqlDbType.VarChar).Value = modelMenu.MenuImage;
            objcmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = modelMenu.Name;
            objcmd.Parameters.Add("@Price", SqlDbType.VarChar).Value = modelMenu.Price;
            objcmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = modelMenu.Description;
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public IActionResult Delete(int MenuID)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Delete_Menu";
            objcmd.Parameters.AddWithValue("@MenuID", MenuID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region CategoryDropDown
        public void CategoryDropDown()
        {
            List<CategoryDropDownModel> list = new List<CategoryDropDownModel>();
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Category_DropDown";
            SqlDataReader objsdr = objcmd.ExecuteReader();

            if (objsdr.HasRows)
            {
                while (objsdr.Read())
                {
                    CategoryDropDownModel model = new CategoryDropDownModel();
                    model.CategoryID = Convert.ToInt32(objsdr["CategoryID"]);
                    model.CategoryName = objsdr["CategoryName"].ToString();
                    list.Add(model);
                }
                ViewBag.CategoryList = list;
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
            objcmd.CommandText = "PR_filter_ByDate_Menu";
            objcmd.Parameters.AddWithValue("startdate", startdate);
            objcmd.Parameters.AddWithValue("enddate", enddate);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("MenuList", dt);
        }
        #endregion
    }
}
