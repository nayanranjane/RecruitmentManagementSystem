using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class JobApplicationController : Controller
    {
        //RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        //JobApplicationServices jobApplicationServices = new JobApplicationServices();

        IDataAccessService<JobApplication, int> jobApplicationService;
        IDataAccessService<JobPost, int> jobPostService;
        IDataAccessService<User,int > userService;
        IDataAccessService<CandidateInfo, int> candidateInfo;
        IDataAccessService<Status, int> statusService;
        public JobApplicationController(IDataAccessService<JobApplication, int> jobApplicationService, IDataAccessService<JobPost, int> jobPostService, IDataAccessService<User, int> userService, IDataAccessService<CandidateInfo, int> candidateInfo, IDataAccessService<Status, int> statusService)
        {
            this.jobApplicationService = jobApplicationService;
            this.jobPostService = jobPostService;
            this.userService = userService;
            this.candidateInfo = candidateInfo;
            this.statusService = statusService;
        }
        AdditionalServices addService = new AdditionalServices();

        [Authorize]
        public async Task<ActionResult> Apply(int? id, string ReturnUrl)
        {
                var candidate = (await userService.GetDataAsync()).ToList().Where(users => users.UserName == Session["Uname"].ToString()).FirstOrDefault();
                if ((await jobApplicationService.GetDataAsync()).ToList().Where(jobApp => jobApp.JobId == id && jobApp.UserId == candidate.UserId).FirstOrDefault() != null)
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
        public async Task<ActionResult> Apply(JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                if (jobApplication.File != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(jobApplication.File.FileName); // .FleName Contain the name of the file with the directory
                    string extension = Path.GetExtension(jobApplication.File.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                    jobApplication.Resume = "~/Documents/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Documents/"), filename);
                    jobApplication.File.SaveAs(filename);
                }
                else
                {
                    jobApplication.Resume = "~/Documents/";
                }
                var isApplied = await jobApplicationService.Create(jobApplication);
                TempData["Applied"] = "Applied";
                return RedirectToAction("Index", "JobPosts");
            }
            else
            {
                return RedirectToAction("Apply", new { id = jobApplication.JobId });
            }
            
        }
        [Authorize(Roles = "Admin,Company")]
        public async Task<ActionResult> Index(int? id)
        {
            List<JobApplication> jobApplication = new List<JobApplication>();
            if (Convert.ToInt32(Session["UID"]) != null && !User.IsInRole("Admin"))
            {

                id = Convert.ToInt32(Session["UID"]);
                var jobPosts = (await jobPostService.GetDataAsync()).ToList().Where(job => job.Company.UserId == id).ToList();
                var jobApplications = await jobApplicationService.GetDataAsync();
                jobApplication = (from post in jobPosts
                                  join app in jobApplications
                                  on post.JobId equals app.JobId
                                  select app).ToList();
            }
            else
            {
                jobApplication = await jobApplicationService.GetDataAsync();
            }
            return View(jobApplication);
        }
        [Authorize (Roles ="Candidate")]
        public async Task<ActionResult> GetMyApplication(int? id)
        {
            var jobApplication = (await jobApplicationService.GetDataAsync()).Where(user => user.UserId == id);
            return View(jobApplication);
        }
        [Authorize(Roles = "Candidate")]
        public ActionResult MyAppliedJobs(int id)
        {
            var jobApplication = addService.getapplicationDetails(id);
            return View(jobApplication);
        }
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            var jobDetails = new JobPost();
            var educationalDetails = new CandidateInfo();
            var jobApplication = (await jobApplicationService.GetDataAsync()).Where(jobApp => jobApp.ApplicationId == id).FirstOrDefault();
            jobDetails = (await jobPostService.GetDataAsync()).Where(job => job.JobId == jobApplication.JobId).FirstOrDefault();
            var status = (await statusService.GetDataAsync());
            educationalDetails = (await candidateInfo.GetDataAsync()).Where(user => user.UserId == jobApplication.UserId).FirstOrDefault();
            ViewBag.Strength = addService.getProfileStrength(educationalDetails, jobApplication, jobDetails);
            ViewBag.JobDetails = jobDetails;
            ViewBag.EducationalDetails = educationalDetails;
            ViewBag.Status = status;
            return View(jobApplication);

        }
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await jobApplicationService.DeleteAsync(id);
            return RedirectToAction("");
        }
        public async Task<ActionResult> AppliedJobs(string userName)
        {
            var applicationList = (await jobApplicationService.GetDataAsync()).Where(application => application.User.UserName==userName);
            return View(applicationList);
        }
        [Authorize(Roles = "Company,Staff,Admin")]
        public async Task<ActionResult> EditStatus(string newstatus,int id)
        {
            try
            {
                var result = await jobApplicationService.GetDataAsync(id);
                result.Status = newstatus;
                var ischanged = await jobApplicationService.UpdateAsync(result, result.ApplicationId);
                if (ischanged)
                {
                    TempData["StatusChange"] = "Status Change";
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Resume(int id)
        {
            var info = await jobApplicationService.GetDataAsync(id);
            return View(info);
        }
    }
}