using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class JobPostServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public JobPost CreateJobPost(JobPost jobPost)
        {
            try
            {
                var result = dbAccess.JobPosts.Add(jobPost);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteJobPost(int id)
        {
            try
            {
                var jobPost = dbAccess.JobPosts.Find(id);
                if (jobPost == null)
                {
                    throw new Exception("post not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.JobPosts.Remove(jobPost);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<JobPost> GetJobPost()
        {
            try
            {
                var postList = dbAccess.JobPosts.ToList();
                return postList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JobPost GetJobPost(int id)
        {
            try
            {
                var postResult = dbAccess.JobPosts.Find(id);
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
        public bool UpdateJobPost(JobPost post, int id)
        {
            try
            {
                var postResult = dbAccess.JobPosts.Find(id);
                if (postResult != null)
                {
                    postResult.Title = post.Title;
                    postResult.CTC = post.CTC;
                    postResult.NoOfOpening = post.NoOfOpening;
                    postResult.Location = post.Location;
                    postResult.LastDate = post.LastDate;
                    postResult.Skill_1 = post.Skill_1;
                    postResult.Skill_2 = post.Skill_2;
                    postResult.Skill_3 = post.Skill_3;
                    postResult.RequiredBatch = post.Skill_3;
                    postResult.Description = post.Description;
                    postResult.HSCMarks = post.HSCMarks;
                    postResult.SSCMarks = post.SSCMarks;
                    postResult.UGMarks = post.UGMarks;
                    postResult.RequiredExperience = post.RequiredExperience;
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