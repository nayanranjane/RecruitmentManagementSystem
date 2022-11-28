using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class JobApplicationController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        JobApplicationServices jobApplicationServices = new JobApplicationServices();
        public ActionResult Apply(int? id, string ReturnUrl)
        {
            if (id != null && Session["Uname"] != null)
            {

                var candidate = dbAccess.Users.ToList().Where(users => users.UserName == Session["Uname"].ToString()).FirstOrDefault();
                if (dbAccess.JobApplications.ToList().Where(jobApp => jobApp.JobId == id && jobApp.UserId == candidate.UserId).FirstOrDefault() != null)
                {
                    TempData["ErrorMessage"] = "<script>alert('Already applied for this job');</script>";
                    if (ReturnUrl != null)
                        return RedirectToAction(ReturnUrl);
                    return RedirectToAction("Index", "JobPosts");
                }
                JobApplication jobApplication = new JobApplication() { JobId = id, UserId = candidate.UserId, ApplicationDate = DateTime.Now };
                return View(jobApplication);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Apply(JobApplication jobApplication)
        {
            var result = dbAccess.JobApplications.Add(jobApplication);
            dbAccess.SaveChanges();
            return RedirectToAction("Index", "JobPosts");
        }
        [Authorize(Roles = "Admin,Company")]
        public ActionResult Index(int? id)
        {
            List<JobApplication> jobApplication = new List<JobApplication>();
            if (Convert.ToInt32(Session["UID"]) != null && Session["Role"].ToString() != "Admin")
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

        public ActionResult GetMyApplication(int? id)
        {
            var jobApplication = dbAccess.JobApplications.ToList().Where(user => user.UserId == id);
            return View(jobApplication);
        }
        public ActionResult Details(int? id)
        {
            var jobDetails = new JobPost();
            var educationalDetails = new CandidateInfo();
            var jobApplication = dbAccess.JobApplications.ToList().Where(jobApp => jobApp.ApplicationId == id).FirstOrDefault();
            jobDetails = dbAccess.JobPosts.ToList().Where(job => job.JobId == jobApplication.JobId).FirstOrDefault();
            educationalDetails = dbAccess.CandidateInfoes.ToList().Where(user => user.UserId == jobApplication.UserId).FirstOrDefault();
            ViewBag.JobDetails = jobDetails;
            ViewBag.EducationalDetails = educationalDetails;
            return View(jobApplication);

        }
        public ActionResult Delete(int? id)
        {
            //var result = dbAccess.JobApplications.Find(id);
            //var isdeleted = dbAccess.JobApplications.Remove(result);
            //dbAccess.SaveChanges()
            var isDeleted = jobApplicationServices.DeleteJobApplication(id);
            return RedirectToAction("");
        }
        public ActionResult AppliedJobs(string userName)
        {
            var applicationList = jobApplicationServices.GetJobApplication().Where(application => application.User.UserName==userName);
            return View(applicationList);
        }
        
    }
}