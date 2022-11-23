using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        public ActionResult Index()
        {
            var Users = dbAccess.Users.ToList();
            return View(Users);

        }
        public ActionResult Create()
        {
            User user = new User();
            var roles = dbAccess.Roles.ToList().Skip(1).ToList();
            ViewBag.roles = roles;
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName); // .FleName Contain the name of the file with the directory
            string extension = Path.GetExtension(user.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            user.Img = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            user.ImageFile.SaveAs(filename);
            var result = dbAccess.Users.Add(user);
            dbAccess.SaveChanges();
            switch (user.RoleId)
            {
                case 2:
                    return RedirectToAction("CreateCandidateInfo", new { id = user.UserId });
                    break;
                case 3:
                    return RedirectToAction("CreateCompanyInformation", new { id = user.UserId });
                    break;
                case 4:
                    return RedirectToAction("CreateStaffInformation", new { id = user.UserId });
                    break;
            }
            return RedirectToAction("Index");

        }

        public ActionResult CreateCandidateInfo(int id)
        {
            CandidateInfo info = new CandidateInfo() { UserId = id };
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateCandidateInfo(CandidateInfo info)
        {
            var result = dbAccess.CandidateInfoes.Add(info);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CreateCompanyInformation(int id)
        {
            Company info = new Company() { UserId = id };
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateCompanyInformation(Company info)
        {
            var result = dbAccess.Companies.Add(info);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CreateStaffInformation(int id)
        {
            Staff info = new Staff() { UserID = id };
            var companies = dbAccess.Companies.ToList();
            ViewBag.companies = companies;
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateStaffInformation(Company info)
        {
            var result = dbAccess.Companies.Add(info);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteUser(int? id)
        {
            var result = dbAccess.Users.Find(id);
            var isRemoved = dbAccess.Users.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailsUser(int? id)
        {
            User result = new User();
            result = dbAccess.Users.Find(id);
            return View(result);
        }
        public ActionResult CandidateDetails(int? id)
        {
            return RedirectToAction("CandidateDetails", "Candidate", new {id=id});

        }

    }
}