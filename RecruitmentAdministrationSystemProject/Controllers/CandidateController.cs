using RecruitmentAdministrationSystemProject.Models;
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
    }
}