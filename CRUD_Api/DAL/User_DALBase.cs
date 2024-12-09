using CRUD_Api.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace CRUD_Api.DAL
{
    public class User_DALBase : DAL_Helpers
    {
        #region Delete
        public bool API_User_Delete(int UserId)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConnString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_DELETE_USER");
                sqldb.AddInParameter(cmd, "@UserId",System.Data.SqlDbType.Int,UserId);
                if (Convert.ToBoolean(sqldb.ExecuteNonQuery(cmd)))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Insert
        public bool API_User_Insert(UserModel UserModel)
        {
            try 
            {
                SqlDatabase sqldb = new SqlDatabase(ConnString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_INSERT_USER");
                sqldb.AddInParameter(cmd, "@UserName", System.Data.SqlDbType.VarChar, UserModel.UserName);
                sqldb.AddInParameter(cmd, "@Contact", System.Data.SqlDbType.VarChar, UserModel.Contact);
                sqldb.AddInParameter(cmd, "@Email", System.Data.SqlDbType.VarChar, UserModel.Email);


                if (Convert.ToBoolean(sqldb.ExecuteNonQuery(cmd)))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion 

        #region Update
        public bool API_User_Update(int UserId,UserModel UserModel)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConnString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_UPDATE_USER");
                sqldb.AddInParameter(cmd, "@UserID", System.Data.SqlDbType.Int, UserModel.UserId);
                sqldb.AddInParameter(cmd, "@UserName", System.Data.SqlDbType.VarChar, UserModel.UserName);
                sqldb.AddInParameter(cmd, "@Contact", System.Data.SqlDbType.VarChar, UserModel.Contact);
                sqldb.AddInParameter(cmd, "@Email", System.Data.SqlDbType.VarChar, UserModel.Email);


                if (Convert.ToBoolean(sqldb.ExecuteNonQuery(cmd)))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region SelectAll
        public List<UserModel> API_User_SelectAll()
        {
            try {
                SqlDatabase sqldb = new SqlDatabase(ConnString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_SELECT_ALL_USER");
                List<UserModel> uModels = new List<UserModel>();

                using (IDataReader dr = sqldb.ExecuteReader(cmd)) {
                    while (dr.Read())
                    {
                        UserModel user = new UserModel();

                        user.UserId = Convert.ToInt32(dr["UserID"].ToString());
                        user.UserName = dr["UserName"].ToString();
                        user.Contact = dr["Contact"].ToString();
                        user.Email = dr["Email"].ToString();
                        uModels.Add(user);
                    }
                }
                return uModels;
            }
            catch {
                return null;
            }
        }
        #endregion

        #region SelectById
        public UserModel API_User_SelectById(int UserId)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(ConnString);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_SELECT_BY_PK_USER");
                sqldb.AddInParameter(cmd,"@UserID",SqlDbType.Int,UserId);
                UserModel user = new UserModel();

                using (IDataReader dr = sqldb.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {

                        user.UserId = Convert.ToInt32(dr["UserID"].ToString());
                        user.UserName = dr["UserName"].ToString();
                        user.Contact = dr["Contact"].ToString();
                        user.Email = dr["Email"].ToString();
                    }
                }
                return user;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
