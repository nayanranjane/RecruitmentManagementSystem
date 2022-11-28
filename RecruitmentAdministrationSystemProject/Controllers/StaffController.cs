using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Controllers
{
  
    public class StaffController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        CompanyServices companyServices = new CompanyServices();
        UserServices userServices = new UserServices();
        public ActionResult CreateStaffInformation(int id)
        {
            Staff info = new Staff() { UserID = id };
            var companies = userServices.GetUsers().Join(companyServices.GetCompany(), user => user.UserId, company => company.UserId, (user, company) => new { Name = user.Name, Id = company.CompanyId });
            ViewBag.companies = companies;
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateStaffInformation(Staff info)
        {
            var result = dbAccess.Staffs.Add(info);
            dbAccess.SaveChanges();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Index(int id)
        {
            var staffs = dbAccess.Staffs.ToList();
            var result = dbAccess.Staffs.ToList().Where(staff => staff.Company.User.UserId == id);
            return View(result);
        }
        public ActionResult Authenticate(int id)
        {
            var staff = dbAccess.Staffs.Find(id);
            staff.isAuthenticated = true;
            dbAccess.SaveChanges();
            return RedirectToAction("Index", new {id=staff.Company.User.UserId});
        }
        public ActionResult Disapprove(int id)
        {
            var staff = dbAccess.Staffs.Find(id);
            staff.isAuthenticated = false;
            dbAccess.SaveChanges();
            return RedirectToAction("Index", new {id=staff.Company.User.UserId });
        }
            
    }
}