using CRUD_Api.DAL;
using CRUD_Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CRUD_Api.BAL
{
    public class User_BALBase
    {
        #region Delete
        public bool API_User_Delete(int UserId) {
            try { 
                User_DALBase dalUser = new User_DALBase();
                if(dalUser.API_User_Delete(UserId))
                    return true;
                else
                    return false;
            }catch(Exception) { 
                return false; 
            }
        }
        #endregion 

        #region Insert
        public bool API_User_Insert(UserModel UserModel)
        {
            try
            {
                User_DALBase dalUser = new User_DALBase();
                if (dalUser.API_User_Insert(UserModel))
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
        public bool API_User_Update(int UserId, UserModel UserModel)
        {
            try
            {
                User_DALBase dalUser = new User_DALBase();
                if (dalUser.API_User_Update(UserId,UserModel))
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
        public List<UserModel> API_User_SelectAll() {
            try {
                User_DALBase dalUser = new User_DALBase();
                List<UserModel> userModels = dalUser.API_User_SelectAll();
                return userModels;
            } catch {
                return null; 
            }
        }
        #endregion

        #region SelectById
        public UserModel API_User_SelectById(int UserId)
        {
            try
            {
                User_DALBase dalUser = new User_DALBase();
                UserModel userModels = dalUser.API_User_SelectById(UserId);
                return userModels;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
