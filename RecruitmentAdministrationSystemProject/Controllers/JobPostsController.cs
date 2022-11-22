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
        [Authorize]
        public ActionResult Create()
        {
            string userName = Session["Uname"].ToString();
            var id = (from user in dbAccess.Users.ToList()
                      join companyinfo in dbAccess.Companies.ToList()
                      on user.UserId equals companyinfo.UserId
                      where user.UserName == userName
                      select companyinfo).FirstOrDefault();
            JobPost jobPost = new JobPost() { CompanyID = id.CompanyId };
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
        public ActionResult Apply(int? id)
        {
            if(id != null && Session["Uname"]!=null)
            {
                var candidate = dbAccess.Users.ToList().Where(users => users.UserName == Session["Uname"].ToString()).FirstOrDefault();
                JobApplication jobApplication = new JobApplication() { JobId = id, UserId = candidate.UserId };
                return View(jobApplication);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Apply(JobApplication jobApplication)
        {
            var result = dbAccess.JobApplications.Add(jobApplication);
            dbAccess.SaveChanges();
            return RedirectToAction("JobApplicationIndex");
        }
        public ActionResult JobApplicationIndex(int? id)
        {
            var jobApplication = dbAccess.JobApplications.ToList();
            return View(jobApplication);
        }

    }
}