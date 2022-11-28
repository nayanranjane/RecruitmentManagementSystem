using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class JobApplicationServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public JobApplication CreateJobApplication(JobApplication jobApplication)
        { 
            try
            {
                var result = dbAccess.JobApplications.Add(jobApplication);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteJobApplication(int? id)
        {
            try
            {
                var jobApplication = dbAccess.JobApplications.Find(id);
                if (jobApplication == null)
                {
                    throw new Exception("Application not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.JobApplications.Remove(jobApplication);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<JobApplication> GetJobApplication()
        {
            try
            {
                var applicationList = dbAccess.JobApplications.ToList();
                return applicationList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JobApplication GetJobApplication(int id)
        {
            try
            {
                var applicationResult = dbAccess.JobApplications.Find(id);
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
        public bool UpdateJobApplication(JobApplication jobApplication, int id)
        {
            try
            {
                var applicationResult = dbAccess.JobApplications.Find(id);
                if (applicationResult != null)
                {
                    applicationResult.AbleToReallocation = jobApplication.AbleToReallocation;
                    applicationResult.PrevCompanyName = jobApplication.PrevCompanyName;
                    applicationResult.WorkExperience = jobApplication.WorkExperience;
                    applicationResult.NoticePeriod = jobApplication.NoticePeriod;
                    applicationResult.Resume = jobApplication.Resume;
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