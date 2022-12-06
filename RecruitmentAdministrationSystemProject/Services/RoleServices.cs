using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class RoleServices:IDataAccessService<Role,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public RoleServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        async Task<Role> IDataAccessService<Role, int>.Create(Role entity)
        {
            try
            {
                var result = dbAccess.Roles.Add(entity);
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

        async Task<bool> IDataAccessService<Role, int>.DeleteAsync(int id)
        {
            try
            {
                var role =await dbAccess.Roles.FindAsync(id);
                if (role == null)
                {
                    throw new Exception("Role not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Roles.Remove(role);
                    await dbAccess.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<Role>> IDataAccessService<Role, int>.GetDataAsync()
        {
            try
            {
                var roleList =await dbAccess.Roles.ToListAsync();
                return roleList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<Role> IDataAccessService<Role, int>.GetDataAsync(int id)
        {
            try
            {
                var roleResult =await dbAccess.Roles.FindAsync(id);
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

        async Task<bool> IDataAccessService<Role, int>.UpdateAsync(Role entity, int id)
        {
            try
            {
                var roleResult =await dbAccess.Roles.FindAsync(id);
                if (roleResult != null)
                {
                    roleResult.RoleName = entity.RoleName;
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
    }
}