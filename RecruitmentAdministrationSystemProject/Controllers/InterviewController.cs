using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class InterviewController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        public ActionResult Index()
        {
            var result = dbAccess.Interviews.ToList();
            return View(result);
        }
        public ActionResult Create(int id, int companyId)
        {
            Interview interview = new Interview() { UserId=id};
            var list = dbAccess.Staffs.ToList();
            var staffList = dbAccess.sp_MyAuthenticatedStaff(companyId).ToList();
        //    var staffList = dbAccess.Users.ToList().Join(dbAccess.Staffs.ToList(), user => user.UserId, staff => staff.UserID, (user, staff) => new { Name = user.Name, Id = staff.StaffId ,isauthenticated = staff.isAuthenticated}).Where(staff=>staff.isauthenticated==true).ToList();
            ViewBag.staffList = staffList;
            return View(interview);
        }
        [HttpPost]
        public ActionResult Create(Interview interview)
        {
            var result = dbAccess.Interviews.Add(interview);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            var result = dbAccess.Interviews.Find(id);
            var isDeleted = dbAccess.Interviews.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult InterviewList(int id)
        {
            var user = dbAccess.Users.Find(id);
            var interviews = dbAccess.Interviews.ToList().Where(inter => inter.Staff.UserID == user.UserId);
            return View(interviews);
        }
        public ActionResult AllInterviews()
        {
          var interviewList = dbAccess.Interviews.ToList();
          return View(interviewList);
         }

    }
}