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
                    postResult.RequiredBatch = post.RequiredBatch;
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
        public IEnumerable<JobPost> SearchJob(string searchString)
        {
            var m = searchString.Split(' ');
            var j = m.ToList();
            List<List<JobPost>> postList = new List<List<JobPost>>();

            var posts = dbAccess.JobPosts.ToList();
            var result = new List<JobPost>();
            foreach (var item in j)
            {
                var temp = (from post in posts
                            where (post.Company.User.Name.ToString().ToLower().Contains(item.ToLower()))
                            || (post.Company.AboutCompany.ToString().ToLower().Contains(item.ToLower()))
                            || (post.Title.ToString().ToLower().Contains(item.ToLower()))
                            || (post.CTC.ToString().Contains(item.ToLower()))
                            || (post.Location.ToString().ToLower().Contains(item.ToLower()))
                            || (post.RequiredBatch.ToString().Contains(item.ToLower()))
                            || (post.Skill_1.ToString().ToLower().Contains(item.ToLower()))
                               || (post.Skill_2.ToString().ToLower().Contains(item.ToLower()))
                                  || (post.Skill_3.ToString().ToLower().Contains(item.ToLower()))
                            select post).ToList();
                postList.Add(temp);
            }
            switch (postList.Count())
            {
                case 0:
                    result = null;
                    break;
                case 1:
                    result = postList[0];
                    break;
                case 2:
                    result = (postList[0].Intersect(postList[1])).ToList();
                    break;
                case 3:
                    result = (postList[0].Intersect(postList[1]).Intersect(postList[2])).ToList();
                    break;
                case 4:
                    result = (postList[0].Intersect(postList[1]).Intersect(postList[2]).Intersect(postList[3])).ToList();
                    break;
                case 5:
                    result = (postList[0].Intersect(postList[1]).Intersect(postList[2]).Intersect(postList[3]).Intersect(postList[4])).ToList();
                    break;
            }
            return result;

        }
    }
}