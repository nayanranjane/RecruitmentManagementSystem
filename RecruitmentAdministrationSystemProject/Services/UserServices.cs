using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class UserServices:IDataAccessService<User,int>
    {
        RecruitmentManagementSystemEntities dbAccess;

        public UserServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        async Task<List<User>> IDataAccessService<User, int>.GetDataAsync()
        {

            try
            {
                var userList =await dbAccess.Users.ToListAsync();
                return userList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<User> IDataAccessService<User, int>.GetDataAsync(int id)
        {
            try
            {
                var userResult =await dbAccess.Users.FindAsync(id);
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

        async Task<bool> IDataAccessService<User, int>.UpdateAsync(User entity, int id)
        {
            try
            {
                var userResult =await dbAccess.Users.FindAsync(id);
                if (userResult != null)
                {
                    userResult.Name = entity.Name;
                    //userResult.UserName = entity.UserName;
                    userResult.Password = entity.Password;
                    userResult.Email = entity.Email;
                    userResult.ConfirmPassword = entity.ConfirmPassword;
                    //userResult.MobileNo = entity.MobileNo;
                    userResult.Location = entity.Location;
                }
                var isUpdated =await dbAccess.SaveChangesAsync();
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

        async Task<User> IDataAccessService<User, int>.Create(User entity)
        {
            try
            {
                var result = dbAccess.Users.Add(entity);
                var isAdded = await dbAccess.SaveChangesAsync();
                if (isAdded > 0)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<bool> IDataAccessService<User, int>.DeleteAsync(int id)
        {
            try
            {
                var user =await dbAccess.Users.FindAsync(id);
                if (user == null)
                {
                    throw new Exception("User not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Users.Remove(user);
                    var idDeleted = await dbAccess.SaveChangesAsync();
                    if (idDeleted > 0)
                    {
                        return true;
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}