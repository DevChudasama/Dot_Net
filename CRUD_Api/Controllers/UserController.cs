using CRUD_Api.BAL;
using CRUD_Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CRUD_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            //If login usrename and password are correct then proceed to generate token

            if (ModelState.IsValid)
            {
                SqlConnection conn = new
               SqlConnection(this._config.GetConnectionString("MyConnectionString"));


                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.CommandText = "PR_SEC_User_Login";
                objCmd.Parameters.AddWithValue("@UserName", loginModel.UserName);
                objCmd.Parameters.AddWithValue("@Password", loginModel.Password);
                SqlDataReader objSDR = objCmd.ExecuteReader();
                DataTable dtLogin = new DataTable();
                Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
                // Check if Data Reader has Rows or not
                // if row(s) does not exists that means either username or password or both are invalid.
                if (!objSDR.HasRows)
                {
                    response.Add("status", "Invalid Credentials");
                    response.Add("token", null);
                    response.Add("data", null);
                }
                else
                {
                    dtLogin.Load(objSDR);
                    foreach (DataRow dr in dtLogin.Rows)
                    {
                        data.Add("UserID", dr["UserID"].ToString());
                        data.Add("UserName", dr["UserName"].ToString());
                        data.Add("Contact", dr["Contact"].ToString());
                        data.Add("Email", dr["Email"].ToString());
                        data.Add("Password", dr["Password"].ToString());
                    }
                    //Prepare token
                    var securityKey = new
                   SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey,
                   SecurityAlgorithms.HmacSha256);
                    var SecToken = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                   claims: null,
                   expires: DateTime.Now.AddMinutes(10),
                   signingCredentials: credentials
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(SecToken);
                    response.Add("status", "success");
                    response.Add("token", token);
                    response.Add("data", data);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


        //#region Delete
        //[HttpDelete]
        //public IActionResult Delete(int UserId)
        //{
        //    User_BALBase balUser = new User_BALBase();
        //    bool IsSuccess = balUser.API_User_Delete(UserId);
        //    Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

        //    if (IsSuccess)
        //    {
        //        response.Add("status", true);
        //        response.Add("message", "Data Deleted Successfully");
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        response.Add("status", false);
        //        response.Add("message", "Some Error Has been Occured");
        //        return Ok(response);
        //    }
        //}
        //#endregion

        //#region Insert
        //[HttpPost]
        //public IActionResult Post([FromForm] UserModel UserModel)
        //{
        //    User_BALBase balUser = new User_BALBase();
        //    bool IsSuccess = balUser.API_User_Insert(UserModel);
        //    Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

        //    if (IsSuccess)
        //    {
        //        response.Add("status", true);
        //        response.Add("message", "Data Inserted Successfully");
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        response.Add("status", false);
        //        response.Add("message", "Some Error Has been Occured");
        //        return Ok(response);
        //    }
        //}
        //#endregion 

        //#region Update
        //[HttpPut]
        //public IActionResult Put(int UserId, [FromForm] UserModel UserModel)
        //{
        //    User_BALBase balUser = new User_BALBase();
        //    UserModel.UserId = UserId;
        //    bool IsSuccess = balUser.API_User_Update(UserId, UserModel);

        //    Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

        //    if (IsSuccess)
        //    {
        //        response.Add("status", true);
        //        response.Add("message", "Data Updated Successfully");
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        response.Add("status", false);
        //        response.Add("message", "Some Error Has been Occured");
        //        return Ok(response);
        //    }
        //}
        //#endregion 

        #region SelectAll
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            User_BALBase balUser = new User_BALBase();
            List<UserModel> ModelUser = balUser.API_User_SelectAll();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (ModelUser.Count > 0 && ModelUser != null)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", ModelUser);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Some Error Has been Occured");
                response.Add("data", null);
                return Ok(response);
            }
        }
        #endregion

        //#region SelectById
        //[HttpGet("{UserId}")]
        //public IActionResult GetById(int UserId)
        //{
        //    User_BALBase balUser = new User_BALBase();
        //    UserModel ModelUser = balUser.API_User_SelectById(UserId);
        //    Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

        //    if (ModelUser != null)
        //    {
        //        response.Add("status", true);
        //        response.Add("message", "Data Found");
        //        response.Add("data", ModelUser);
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        response.Add("status", false);
        //        response.Add("message", "Data Not Found");
        //        response.Add("data", null);
        //        return Ok(response);
        //    }
        //}
        //#endregion
    }
}