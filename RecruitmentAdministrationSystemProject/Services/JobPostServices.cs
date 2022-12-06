using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace RecruitmentAdministrationSystemProject.Services
{
    [Authorize]
    public class JobPostServices:IDataAccessService<JobPost,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public JobPostServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

       
        async Task<JobPost> IDataAccessService<JobPost, int>.Create(JobPost entity)
        {
            try
            {
                var result = dbAccess.JobPosts.Add(entity);
                var idAdded = await dbAccess.SaveChangesAsync();
                if (idAdded > 0)
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

        async Task<bool> IDataAccessService<JobPost, int>.DeleteAsync(int id)
        {
            try
            {
                var jobPost =await dbAccess.JobPosts.FindAsync(id);
                if (jobPost == null)
                {
                    throw new Exception("post not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.JobPosts.Remove(jobPost);
                    var result = await dbAccess.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<JobPost>> IDataAccessService<JobPost, int>.GetDataAsync()
        {
            try
            {
                var postList =await dbAccess.JobPosts.ToListAsync();
                return postList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<JobPost> IDataAccessService<JobPost, int>.GetDataAsync(int id)
        {
            try
            {
                var postResult = await dbAccess.JobPosts.FindAsync(id);
                if (postResult != null)
                {
                    return postResult;
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

        async Task<bool> IDataAccessService<JobPost, int>.UpdateAsync(JobPost entity, int id)
        {
            try
            {
                var postResult =await dbAccess.JobPosts.FindAsync(id);
                if (postResult != null)
                {
                    postResult.Title = entity.Title;
                    postResult.CTC = entity.CTC;
                    postResult.NoOfOpening = entity.NoOfOpening;
                    postResult.Location = entity.Location;
                    postResult.LastDate = entity.LastDate;
                    postResult.Skill_1 = entity.Skill_1;
                    postResult.Skill_2 = entity.Skill_2;
                    postResult.Skill_3 = entity.Skill_3;
                    postResult.RequiredBatch = entity.RequiredBatch;
                    postResult.Description = entity.Description;
                    postResult.HSCMarks = entity.HSCMarks;
                    postResult.SSCMarks = entity.SSCMarks;
                    postResult.UGMarks = entity.UGMarks;
                    postResult.RequiredExperience = entity.RequiredExperience;
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