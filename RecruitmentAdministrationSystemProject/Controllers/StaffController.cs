using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Controllers
{
  
    public class StaffController : Controller
    {
        //RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        IDataAccessService<Company, int> companyService;
        IDataAccessService<User,int> userService;
        IDataAccessService<Staff, int> staffService;

        public StaffController(IDataAccessService<Company, int> companyService, IDataAccessService<User, int> userService, IDataAccessService<Staff, int> staffService)
        {
            this.companyService = companyService;
            this.userService = userService;
            this.staffService = staffService;
        }

        public async Task<ActionResult> CreateStaffInformation(int id)
        {
            Staff info = new Staff() { UserID = id };
            var companies =(await userService.GetDataAsync()).ToList().Join((await companyService.GetDataAsync()).ToList(), user => user.UserId, company => company.UserId, (user, company) => new { Name = user.Name, Id = company.CompanyId });
            ViewBag.companies = companies;
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateStaffInformation(Staff info)
        {
            var result = staffService.Create(info);
            return RedirectToAction("Login", "Account");
        }
        public async Task<ActionResult> Index(int id)
        {
            var staffs =(await staffService.GetDataAsync()).ToList();
            var result = (await staffService.GetDataAsync()).ToList().Where(staff => staff.Company.User.UserId == id&&staff.Company.UserId== Convert.ToInt32(Session["UID"]));
            return View(result);
        }
        public async Task<ActionResult> Authenticate(int id)
        {
            var staff =await staffService.GetDataAsync(id);
            staff.isAuthenticated = true;
            var result = await staffService.UpdateAsync(staff,staff.StaffId);
            return RedirectToAction("Index", new {id=staff.Company.User.UserId});
        }
        public async Task<ActionResult> Disapprove(int id)
        {
            var staff = await staffService.GetDataAsync(id);
            staff.isAuthenticated = false;
            var result = await staffService.UpdateAsync(staff, staff.StaffId);
            return RedirectToAction("Index", new {id=staff.Company.User.UserId });
        }
            
    }
}