using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class AdditionalServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

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
        public IEnumerable<sp_ApplicationDetails_Result> getapplicationDetails(int id)
        {
            var applicationDetails = dbAccess.sp_ApplicationDetails(id).ToList();
            return applicationDetails;
        }
        public IEnumerable<sp_MyAuthenticatedStaff_Result> getAuthenticatedStaff(int id)
        {
            var authenticatedStaff = dbAccess.sp_MyAuthenticatedStaff(id).ToList();
            return authenticatedStaff;
        }
        public int getProfileStrength(CandidateInfo candidateInfo,JobApplication jobApplication,JobPost jobPost)
        {
            int strenght = 0;
            if (candidateInfo.SSC_Marks >= jobPost.SSCMarks)
                strenght++;
            if (candidateInfo.HSC_Marks >= jobPost.HSCMarks)
                strenght++;
            if (candidateInfo.UG_Marks >= jobPost.UGMarks)
                strenght++;
            if (candidateInfo.Skill_1 == jobPost.Skill_1)
                strenght++;
            if (candidateInfo.Skill_2 == jobPost.Skill_2)
                strenght++;
            if (candidateInfo.Skill_3 == jobPost.Skill_3)
                strenght++;
            if (candidateInfo.UG_PassingYear == jobPost.RequiredBatch)
                strenght++;
            if (jobApplication.WorkExperience == jobPost.RequiredExperience)
                strenght++;
            return strenght;
        }
    }
}