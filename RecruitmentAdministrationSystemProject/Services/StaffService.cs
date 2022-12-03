using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class StaffService : IDataAccessService<Staff, int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public StaffService(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }
        async Task<bool> IDataAccessService<Staff, int>.Create(Staff entity)
        {
            try
            {
                var result = dbAccess.Staffs.Add(entity);
                var isAdded = await dbAccess.SaveChangesAsync();
                if (isAdded > 0)
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

        async Task<bool> IDataAccessService<Staff, int>.DeleteAsync(int id)
        {
            try
            {
                var staff = await dbAccess.Staffs.FindAsync(id);
                if (staff == null)
                {
                    throw new Exception("Staff not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Staffs.Remove(staff);
                    await dbAccess.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<Staff>> IDataAccessService<Staff, int>.GetDataAsync()
        {
            try
            {
                var roleList = await dbAccess.Staffs.ToListAsync();
                return roleList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<Staff> IDataAccessService<Staff, int>.GetDataAsync(int id)
        {
            try
            {
                var staffResult = await dbAccess.Staffs.FindAsync(id);
                if (staffResult != null)
                {
                    return staffResult;
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

        async Task<bool> IDataAccessService<Staff, int>.UpdateAsync(Staff entity, int id)
        {
            try
            {
                var staffResult = await dbAccess.Staffs.FindAsync(id);
                if (staffResult != null)
                {
                    staffResult.CompanyId = entity.CompanyId;
                    staffResult.isAuthenticated = entity.isAuthenticated;
                    staffResult.Designation = entity.Designation;
                }
                var isUpdated = await dbAccess.SaveChangesAsync();
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