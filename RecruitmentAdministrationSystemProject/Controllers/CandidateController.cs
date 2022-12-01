using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class CandidateController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        SkillsServices skillsServices = new SkillsServices();
       
        public ActionResult Index()
        {
            var candidates = dbAccess.CandidateInfoes.ToList();
            return View(candidates);
        }
        public ActionResult CandidateDetails(int? id)
        {
            var candidates = dbAccess.CandidateInfoes.ToList().Where(candidateUser => candidateUser.UserId == id).ToList();
            var Candidate = new CandidateInfo();
            if(candidates.Count()!=0)
                Candidate = candidates.First();
            return View(Candidate);

        }

        public ActionResult DeleteCandidate(int? id)
        {
            var result = dbAccess.Users.Find(id);
            var isDeleted = dbAccess.Users.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("index");

        }
        public ActionResult CreateCandidateInfo(int id)
        {
            CandidateInfo info = new CandidateInfo() { UserId = id };
            var skills = skillsServices.GetAllSkills();
            ViewBag.Skills = skills;
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateCandidateInfo(CandidateInfo info)
        {
            var result = dbAccess.CandidateInfoes.Add(info);
            var isCreated = dbAccess.SaveChanges();
            if (isCreated > 0)
            {
                TempData["UserCreated"] = "UserCreated";
            }
            return RedirectToAction("Login", "Account");
        }

    }
}