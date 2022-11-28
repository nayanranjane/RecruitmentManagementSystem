using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class RoleServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public Role CreateRole(Role role)
        {
            try
            {
                var result = dbAccess.Roles.Add(role);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteRole(int id)
        {
            try
            {
                var role = dbAccess.Roles.Find(id);
                if (role == null)
                {
                    throw new Exception("Role not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Roles.Remove(role);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Role> GetRole()
        {
            try
            {
                var roleList = dbAccess.Roles.ToList();
                return roleList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Role GetRole(int id)
        {
            try
            {
                var roleResult = dbAccess.Roles.Find(id);
                if (roleResult != null)
                {
                    return roleResult;
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
        public bool UpdateUser(Role role, int id)
        {
            try
            {
                var roleResult = dbAccess.Roles.Find(id);
                if (roleResult != null)
                {
                    roleResult.RoleName = role.RoleName;
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