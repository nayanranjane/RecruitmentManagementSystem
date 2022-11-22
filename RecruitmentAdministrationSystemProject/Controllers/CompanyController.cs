using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var Companies = (from user in dbAccess.Users
                             join company in dbAccess.Companies
                             on user.UserId equals company.UserId
                             join role in dbAccess.Roles
                             on user.RoleId equals role.RoleId
                             where role.RoleName.ToLower()=="company".ToLower()
                             select new CompanyDetails
                             {
                                 UserId = user.UserId,
                                 CompanyId = company.CompanyId,
                                 Name = user.Name,
                                 MobileNo = user.MobileNo,
                                 Location = user.Location,
                                 Email = user.Email,
                                 Img = user.Img,
                                 CompanyUrl = company.CompanyUrl,
                                 EmployeeCount = company.EmployeeCount,
                                 AboutCompany = company.AboutCompany,
                                 FoundDate = company.FoundDate
                             }).ToList();
            ViewBag.Companies = Companies;
            return View();
        }
        public ActionResult DeleteCompany(int? id)
        {
            var result = dbAccess.Users.Find(id);
            var isDeleted = dbAccess.Users.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index","Company");
        }
    }
}