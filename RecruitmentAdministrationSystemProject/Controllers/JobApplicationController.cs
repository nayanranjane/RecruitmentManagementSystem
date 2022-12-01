using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
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
        [HttpPost]
        public ActionResult Apply(JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(jobApplication.File.FileName); // .FleName Contain the name of the file with the directory
                string extension = Path.GetExtension(jobApplication.File.FileName);
                filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                jobApplication.Resume = "~/Documents/" + filename;
                filename = Path.Combine(Server.MapPath("~/Documents/"), filename);
                jobApplication.File.SaveAs(filename);
                var result = dbAccess.JobApplications.Add(jobApplication);
                dbAccess.SaveChanges();
                TempData["Applied"] = "Applied";
                return RedirectToAction("Index", "JobPosts");
            }
            else
            {
                return RedirectToAction("Apply", new { id = jobApplication.JobId });
            }
            
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

        public ActionResult MyAppliedJobs(int id)
        {
            var jobApplication = dbAccess.sp_ApplicationDetails(id).ToList();
            return View(jobApplication);
        }
        public ActionResult Details(int? id)
        {
            var jobDetails = new JobPost();
            var educationalDetails = new CandidateInfo();
            var jobApplication = dbAccess.JobApplications.ToList().Where(jobApp => jobApp.ApplicationId == id).FirstOrDefault();
            jobDetails = dbAccess.JobPosts.ToList().Where(job => job.JobId == jobApplication.JobId).FirstOrDefault();
            var status = dbAccess.Status.ToList();
            educationalDetails = dbAccess.CandidateInfoes.ToList().Where(user => user.UserId == jobApplication.UserId).FirstOrDefault();
            ViewBag.JobDetails = jobDetails;
            ViewBag.EducationalDetails = educationalDetails;
            ViewBag.Status = status;
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
        public ActionResult EditStatus(string newstatus,int id)
        {
                var result = dbAccess.JobApplications.Find(id);
                result.Status = newstatus;
                int ischanged = dbAccess.SaveChanges();
            if (ischanged > 0)
            {
                TempData["StatusChange"] = "Status Change";
            }
                //return RedirectToAction("Details", new { id = id });
                return RedirectToAction("Index");

           
        }
    }
}