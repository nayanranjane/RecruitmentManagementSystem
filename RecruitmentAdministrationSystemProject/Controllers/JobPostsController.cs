using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Views
{
    [Authorize]
    public class JobPostsController : Controller
    {
      
        // GET: JobPosts
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        public ActionResult Index(int? id)
        {
            List<JobPost> jobPosts = new List<JobPost>();
            if(id == null)
            {
                jobPosts = dbAccess.JobPosts.ToList();
            }
            else
            {
                jobPosts = dbAccess.JobPosts.Where(post => post.CompanyID == id).ToList();
            }
            return View(jobPosts);
        }
        [Authorize(Roles ="Admin,Company")]
        public ActionResult Create()
        {
            string userName = Session["Uname"].ToString();
            var id = (from user in dbAccess.Users.ToList()
                      join companyinfo in dbAccess.Companies.ToList()
                      on user.UserId equals companyinfo.UserId
                      where user.UserName == userName
                      select companyinfo).FirstOrDefault();
            JobPost jobPost = new JobPost() { CompanyID = id.CompanyId ,PostDate=DateTime.Now};
            return View(jobPost);
        }
        [HttpPost]
        public ActionResult Create(JobPost jobPost)
        {
            var result = dbAccess.JobPosts.Add(jobPost);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            var result = dbAccess.JobPosts.Find(id);
            dbAccess.JobPosts.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult Apply(int? id, string ReturnUrl)
        {
            if(id != null && Session["Uname"]!=null)
            { 
                
                var candidate = dbAccess.Users.ToList().Where(users => users.UserName == Session["Uname"].ToString()).FirstOrDefault();
                if (dbAccess.JobApplications.ToList().Where(jobApp => jobApp.JobId == id&& jobApp.UserId == candidate.UserId).FirstOrDefault()!=null)
                {
                    TempData["ErrorMessage"] = "<script>alert('Already applied for this job');</script>";
                    if (ReturnUrl!=null)
                        return RedirectToAction(ReturnUrl);
                    return RedirectToAction("Index", "JobPosts");
                }
                    JobApplication jobApplication = new JobApplication() { JobId = id, UserId = candidate.UserId ,ApplicationDate=DateTime.Now};
                return View(jobApplication);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Apply(JobApplication jobApplication)
        {
            var result = dbAccess.JobApplications.Add(jobApplication);
            dbAccess.SaveChanges();
            return RedirectToAction("Index","JobPosts");
        }
        [Authorize(Roles ="Admin,Company")]
        public ActionResult JobApplicationIndex(int? id)
        {
            List<JobApplication> jobApplication = new List<JobApplication>();
            if (Convert.ToInt32(Session["UID"])!=null && Session["Role"].ToString()!="Admin")
            {
                id = Convert.ToInt32(Session["UID"]);
                var jobPosts = dbAccess.JobPosts.ToList().Where(job => job.Company.UserId == id).ToList();
                var jobApplications = dbAccess.JobApplications.ToList();
                jobApplication = (from post in jobPosts
                                  join app in jobApplications
                                  on post.JobId equals app.JobId
                                  select app).ToList();
            }
            else
            {
                jobApplication = dbAccess.JobApplications.ToList();
            }
            return View(jobApplication);
        }
        public ActionResult ApplicationDetails(int? id)
        {
            var jobDetails = new JobPost();
            var educationalDetails = new CandidateInfo();
            var jobApplication = dbAccess.JobApplications.ToList().Where(jobApp=>jobApp.ApplicationId==id).FirstOrDefault();
            jobDetails = dbAccess.JobPosts.ToList().Where(job => job.JobId == jobApplication.JobId).FirstOrDefault();
            educationalDetails = dbAccess.CandidateInfoes.ToList().Where(user => user.UserId == jobApplication.UserId).FirstOrDefault();
            ViewBag.JobDetails = jobDetails;
            ViewBag.EducationalDetails = educationalDetails;
            return View(jobApplication);

        }
        public ActionResult ApplicationDelete(int? id)
        {
            var result = dbAccess.JobApplications.Find(id);
            var isdeleted = dbAccess.JobApplications.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("JobApplicationIndex", "JobPosts");
        }

    }
}