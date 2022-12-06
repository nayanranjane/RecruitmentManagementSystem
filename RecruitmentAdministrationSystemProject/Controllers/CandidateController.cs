using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    [Authorize]
    public class CandidateController : Controller
    {
        IDataAccessService<Skill, int> skillService;
        IDataAccessService<CandidateInfo,int> candidateInfoService;
        IDataAccessService<User,int> userService;
        public CandidateController(IDataAccessService<Skill, int> skillService, IDataAccessService<CandidateInfo, int> candidateInfoService, IDataAccessService<User, int> userService)
        {
            this.skillService = skillService;
            this.candidateInfoService = candidateInfoService;
            this.userService = userService;
        }
        public async Task<ActionResult> Index()
        {
            var candidates = (await candidateInfoService.GetDataAsync()).ToList();
            return View(candidates);
        }
        public async Task<ActionResult> CandidateDetails(int? id)
        {
            var candidate = (await candidateInfoService.GetDataAsync()).ToList().Where(candidateUser => candidateUser.UserId == id).ToList().FirstOrDefault();
            if (candidate != null) { return View(candidate); }
            return RedirectToAction("Index", "Home");

        }

        public async Task<ActionResult> DeleteCandidate(int id)
        {
            var result =(await userService.DeleteAsync(id));
            return RedirectToAction("index");

        }
        [AllowAnonymous]
        public async Task<ActionResult> CreateCandidateInfo(int id)
        {
            CandidateInfo info = new CandidateInfo() { UserId = id };
            var skills = (await skillService.GetDataAsync()).ToList();
            ViewBag.Skills = skills;
            return View(info);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateCandidateInfo(CandidateInfo info)
        {
            var isCreated =await candidateInfoService.Create(info);
            if (isCreated!=null)
            {
                TempData["UserCreated"] = "UserCreated";
            }
            return RedirectToAction("Login", "Account");
        }

    }
}