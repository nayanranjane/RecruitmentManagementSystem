using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Views
{

    public class JobPostsController : Controller
    {
      
        // GET: JobPosts
        //RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        IDataAccessService<JobPost, int> jobService;
        IDataAccessService<Skill, int> skillService;
        IDataAccessService<Location,int> locationService;
        IDataAccessService<User, int> userService;
        IDataAccessService<Company, int> companyService;

        public JobPostsController(IDataAccessService<JobPost, int> jobService, IDataAccessService<Skill, int> skillService, IDataAccessService<Location, int> locationService, IDataAccessService<User, int> userService, IDataAccessService<Company, int> companyService)
        {
            this.jobService = jobService;
            this.skillService = skillService;
            this.locationService = locationService;
            this.userService = userService;
            this.companyService = companyService;
        }
        AdditionalServices addService = new AdditionalServices();
        async public Task<ActionResult> Index()
        {
            List<JobPost> jobPosts = new List<JobPost>();
            jobPosts = await jobService.GetDataAsync();
            return View(jobPosts);
        }
        [Authorize(Roles ="Company")]
        async public Task<ActionResult> Create()
        {
            var id = (from user in await userService.GetDataAsync()
                      join companyinfo in await companyService.GetDataAsync()
                      on user.UserId equals companyinfo.UserId
                      where user.UserName == (@User.Identity.Name).ToString()
            select companyinfo).FirstOrDefault();
            var skills = await skillService.GetDataAsync();
            var locations =await locationService.GetDataAsync();
            ViewBag.location = locations;
            ViewBag.Skills = skills;
            JobPost jobPost = new JobPost() { CompanyID = id.CompanyId ,PostDate=DateTime.Now};
            return View(jobPost);
        }
        [HttpPost]
        public async Task<ActionResult> Create(JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                var result =await jobService.Create(jobPost);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        [Authorize (Roles ="Company,Staff")]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await jobService.DeleteAsync(id);
            return RedirectToAction("Index","Home");
        }
        async public Task<ActionResult> Details(int id)
        {
            if(id != null)
            {
                JobPost jobPost = await jobService.GetDataAsync(id);
                return View(jobPost);
            }
            return RedirectToAction("Index");
        }
        [Authorize (Roles ="Company")]
        async public Task<ActionResult> MyJobPosts(string username)
        {
            var jobList = (await jobService.GetDataAsync()).Where(post => post.Company.User.UserName == username);
            return View(jobList);

        }

        [HttpPost]
        public async Task<ActionResult> Index2(string search)
        {
            List<JobPost> jobPosts = new List<JobPost>();
            if (search != null)
            {
                jobPosts = addService.SearchJob(search).ToList();
            }
            else
            {
                jobPosts = await jobService.GetDataAsync();
            }

            return View(jobPosts);
        }

    }
}