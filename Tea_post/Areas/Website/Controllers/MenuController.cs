﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Tea_post.Areas.Website.Controllers
{
    [Area("Website")]
    [Route("Website/[Controller]/[Action]")]
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
            return View("_MenuList", dt);
        }
        #endregion
    }
}
