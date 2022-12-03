using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class InterviewController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        IDataAccessService<Interview, int> interviewService;
        IDataAccessService<Staff, int> staffService;
        IDataAccessService<Status, int> statusService;
        IDataAccessService<Company, int> companyService;
        IDataAccessService<User, int> userService;
        public InterviewController(IDataAccessService<Interview, int> interviewService, IDataAccessService<Staff, int> staffService, IDataAccessService<Status, int> statusService, IDataAccessService<Company, int> companyService, IDataAccessService<User, int> userService)
        {
            this.interviewService = interviewService;
            this.staffService = staffService;
            this.statusService = statusService;
            this.companyService = companyService;
            this.userService = userService;
        }
        AdditionalServices additionalServices = new AdditionalServices();
        public async Task<ActionResult> Index()
        {
            var company = (await companyService.GetDataAsync()).Where(comp => comp.User.UserId == Convert.ToInt32(Session["UID"])).FirstOrDefault();
            var result = (await interviewService.GetDataAsync()).Where(interview => interview.Staff.CompanyId == company.CompanyId);
            return View(result);
        }
        public ActionResult Create(int id, int companyId)
        {
            Interview interview = new Interview() { UserId=id};
            var staffList = additionalServices.getAuthenticatedStaff(companyId);
        //    var staffList = dbAccess.Users.ToList().Join(dbAccess.Staffs.ToList(), user => user.UserId, staff => staff.UserID, (user, staff) => new { Name = user.Name, Id = staff.StaffId ,isauthenticated = staff.isAuthenticated}).Where(staff=>staff.isauthenticated==true).ToList();
            ViewBag.staffList = staffList;
            return View(interview);
        }
        [HttpPost]
        public ActionResult Create(Interview interview)
        {
            if (ModelState.IsValid)
            {
                var isCreated = interviewService.Create(interview);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted =await interviewService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> InterviewList(int id)
        {
            var user =await userService.GetDataAsync(id);
            var interviews = (await interviewService.GetDataAsync()).ToList().Where(inter => inter.Staff.UserID == user.UserId);
            return View(interviews);
        }
        public async Task<ActionResult> AllInterviews()
        {
            var interviewList = await interviewService.GetDataAsync();
            return View(interviewList);
        }
        public async Task<ActionResult> MyInterviews()
        {
            var interviewList = (await interviewService.GetDataAsync()).Where(interview=>interview.User.UserId== Convert.ToInt32(Session["UID"]));
            return View(interviewList);
        }
        public async Task<ActionResult> ScheduledInterview()
        {
         //   var staffDetails = (await staffService.GetDataAsync()).Where(staff => staff.User.UserId == Convert.ToInt32(Session["UID"])).FirstOrDefault();
            var interviewList = (await interviewService.GetDataAsync()).Where(interview => interview.Staff.User.UserId == Convert.ToInt32(Session["UID"]));
            return View(interviewList);
        }
        public async Task<ActionResult> Remark(int id)
        {
            var status = (await statusService.GetDataAsync());
            var interview =await interviewService.GetDataAsync(id);
            ViewBag.Status = status;
            return View(interview);
        }
        [HttpPost]
        public async Task<ActionResult> Remark(Interview interview)
        {
            var result = interviewService.UpdateAsync(interview, interview.InterviewID);
            return RedirectToAction("ScheduledInterview");
        }
    }
}