using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Services
{
    [Authorize]
    public class JobApplicationServices:IDataAccessService<JobApplication,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public JobApplicationServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

      
        async Task<bool> IDataAccessService<JobApplication, int>.Create(JobApplication entity)
        {
            try
            {
                var result = dbAccess.JobApplications.Add(entity);
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

        async Task<bool> IDataAccessService<JobApplication, int>.DeleteAsync(int id)
        {
            try
            {
                var jobApplication =await dbAccess.JobApplications.FindAsync(id);
                if (jobApplication == null)
                {
                    throw new Exception("Application not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.JobApplications.Remove(jobApplication);
                    dbAccess.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<JobApplication>> IDataAccessService<JobApplication, int>.GetDataAsync()
        {
            try
            {
                var applicationList = await dbAccess.JobApplications.ToListAsync();
                return applicationList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<JobApplication> IDataAccessService<JobApplication, int>.GetDataAsync(int id)
        {
            try
            {
                var applicationResult =await dbAccess.JobApplications.FindAsync(id);
                if (applicationResult != null)
                {
                    return applicationResult;
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

        async Task<bool> IDataAccessService<JobApplication, int>.UpdateAsync(JobApplication entity, int id)
        {
            try
            {
                var applicationResult =await dbAccess.JobApplications.FindAsync(id);
                if (applicationResult != null)
                {
                    applicationResult.AbleToReallocation = entity.AbleToReallocation;
                    applicationResult.PrevCompanyName = entity.PrevCompanyName;
                    applicationResult.WorkExperience = entity.WorkExperience;
                    applicationResult.NoticePeriod = entity.NoticePeriod;
                    applicationResult.Resume = entity.Resume;
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