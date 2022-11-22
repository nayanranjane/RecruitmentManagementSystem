using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Views
{
    public class JobPostsController : Controller
    {
        // GET: JobPosts
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        public ActionResult Index()
        {
            return View();
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
    }
}