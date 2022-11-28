using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class UserServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public User CreateUser(User user)
        {
            try
            {
                var result = dbAccess.Users.Add(user);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteUser(int id)
        {
            try
            {
                var user = dbAccess.Users.Find(id);
                if (user == null)
                {
                    throw new Exception("User not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Users.Remove(user);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<User> GetUsers()
        {
            try
            {
                var userList = dbAccess.Users.ToList();
                return userList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public User GetUser(int id)
        {
            try
            {
                var userResult = dbAccess.Users.Find(id);
                if (userResult != null)
                {
                    return userResult;
                }
                else
                {
                    throw new Exception("Record not found");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateUser(User user,int id)
        {
            try
            {
                var userResult = dbAccess.Users.Find(id);
                if(userResult != null)
                {
                    userResult.Name = user.Name;
                    userResult.UserName = user.UserName;
                    userResult.Password = user.Password;
                    userResult.Email = user.Email;
                    userResult.Img = user.Img;
                    userResult.MobileNo = user.MobileNo;
                    userResult.Location = user.Location;
                }
                var isUpdated = dbAccess.SaveChanges();
                if (isUpdated > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}