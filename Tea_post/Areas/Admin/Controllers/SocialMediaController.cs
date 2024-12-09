using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Tea_post.Areas.Admin.Models;

namespace Tea_post.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class SocialMediaController : Controller
    {


        #region IConfiguration
        private IConfiguration Configuration;
        public SocialMediaController(IConfiguration _configuration)
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
            objcmd.CommandText = "PR_Read_SocialMedia";
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("SocialMediaList", dt);
        }
        #endregion

        public IActionResult Add()
        {
            return View("SocialMediaAddEdit");
        }

        #region Edit
        public IActionResult Edit(int SocialMediaID = 0)
        {
            SocialMediaModel Cmodel = new SocialMediaModel();

            if (SocialMediaID > 0)
            {
                string connectionstr = Configuration.GetConnectionString("MyStr");
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionstr);
                conn.Open();
                SqlCommand objcmd = conn.CreateCommand();
                objcmd.CommandType = CommandType.StoredProcedure;
                objcmd.CommandText = "PR_SelectByPK_SocialMedia";
                objcmd.Parameters.AddWithValue("@SocialMediaID", SocialMediaID);
                SqlDataReader objsdr = objcmd.ExecuteReader();
                dt.Load(objsdr);


                foreach (DataRow dr in dt.Rows)
                {
                    Cmodel.SocialMediaID = int.Parse(dr["SocialMediaID"].ToString());
                    Cmodel.SocialMediaImage = dr["SocialMediaImage"].ToString();
                    Cmodel.SocialMediaLink = dr["SocialMediaLink"].ToString();
                }

                return View("SocialMediaAddEdit", Cmodel);
            }
            return View("SocialMediaAddEdit", Cmodel);

        }
        #endregion


        //changes=====>
        #region Add/Edit
        [HttpPost]
        public IActionResult Save(SocialMediaModel modelSocialMedia)
        {
            if (modelSocialMedia.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelSocialMedia.File.FileName);
                modelSocialMedia.SocialMediaImage = FilePath.Replace("wwwroot\\", "/") + "/" + modelSocialMedia.File.FileName;

                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelSocialMedia.File.CopyTo(fileStream);
                }
            }

            string connectionstr = Configuration.GetConnectionString("MyStr");

            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            if (modelSocialMedia.SocialMediaID == null)
            {
                objcmd.CommandText = "PR_INSERT_SocialMedia";

            }
            else
            {
                objcmd.CommandText = "PR_Update_SocialMedia";
                objcmd.Parameters.Add("@SocialMediaID", SqlDbType.VarChar).Value = modelSocialMedia.SocialMediaID;
            }
            objcmd.Parameters.Add("@SocialMediaImage", SqlDbType.VarChar).Value = modelSocialMedia.SocialMediaImage;
            objcmd.Parameters.Add("@SocialMediaLink", SqlDbType.VarChar).Value = modelSocialMedia.SocialMediaLink;

            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public IActionResult Delete(int SocialMediaID)
        {
            string connectionstr = Configuration.GetConnectionString("MyStr");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_Delete_SocialMedia";
            objcmd.Parameters.AddWithValue("@SocialMediaID", SocialMediaID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction(nameof(Index));
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
            objcmd.CommandText = "PR_filter_ByDate_SocialMedia";
            objcmd.Parameters.AddWithValue("startdate", startdate);
            objcmd.Parameters.AddWithValue("enddate", enddate);
            SqlDataReader objsdr = objcmd.ExecuteReader();
            dt.Load(objsdr);
            conn.Close();
            return View("SocialMediaList", dt);
        }
        #endregion
    }
}
