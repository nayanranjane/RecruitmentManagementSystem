using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Views
{
    [Authorize]
    public class JobPostsController : Controller
    {
      
        // GET: JobPosts
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        JobPostServices jobPostServices = new JobPostServices();
        SkillsServices skillsServices = new SkillsServices();   
        public ActionResult Index()
        {
            List<JobPost> jobPosts = new List<JobPost>();
            jobPosts = dbAccess.JobPosts.ToList();
           
            return View(jobPosts);
        }
        [Authorize(Roles ="Company")]
        public ActionResult Create()
        {
            string userName = Session["Uname"].ToString();
            var id = (from user in dbAccess.Users.ToList()
                      join companyinfo in dbAccess.Companies.ToList()
                      on user.UserId equals companyinfo.UserId
                      where user.UserName == userName
                      select companyinfo).FirstOrDefault();
            var skills = skillsServices.GetAllSkills();
            ViewBag.Skills = skills;
            JobPost jobPost = new JobPost() { CompanyID = id.CompanyId ,PostDate=DateTime.Now};
            return View(jobPost);
        }
        [HttpPost]
        public ActionResult Create(JobPost jobPost)
        {
            var result = dbAccess.JobPosts.Add(jobPost);
            dbAccess.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Delete(int? id)
        {
            var result = dbAccess.JobPosts.Find(id);
            dbAccess.JobPosts.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                JobPost jobPost = dbAccess.JobPosts.Find(id);
                return View(jobPost);
            }
            return RedirectToAction("Index");
        }
        public ActionResult MyJobPosts(string username)
        {
            var jobList = jobPostServices.GetJobPost().Where(post => post.Company.User.UserName == username);
            return View(jobList);

        }

        [HttpPost]
        public ActionResult Index2(string search)
        {
            List<JobPost> jobPosts = new List<JobPost>();
            if (search != null)
            {
                 jobPosts = jobPostServices.SearchJob(search).ToList();
            }
            else
            {
                jobPosts = jobPostServices.GetJobPost().ToList() ;
            }

            return View(jobPosts);
        }
        public ActionResult ShowDetails(int? id)
        {
            return RedirectToAction("Index", new { id = id });

        }


    }
}