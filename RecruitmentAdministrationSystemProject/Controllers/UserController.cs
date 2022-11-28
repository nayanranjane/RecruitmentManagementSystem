using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        UserServices userServices = new UserServices(); 
        RoleServices roleServices = new RoleServices();
        public ActionResult Index()
        {
            var Users = userServices.GetUsers();
            return View(Users);

        }


        public ActionResult Create()
        {
            User user = new User();
            var roles = roleServices.GetRole().Skip(1).ToList();
            ViewBag.roles = roles;
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (user.ImageFile!=null)
            {
            string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName); // .FleName Contain the name of the file with the directory
            string extension = Path.GetExtension(user.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            user.Img = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            user.ImageFile.SaveAs(filename);
            }
            var result = userServices.CreateUser(user);
            switch (user.RoleId)
            {
                case 2:
                    return RedirectToAction("CreateCandidateInfo","Candidate", new { id = user.UserId });
                    break;
                case 3:
                    return RedirectToAction("CreateCompanyInformation","Company", new { id = user.UserId });
                    break;
                case 4:
                    return RedirectToAction("CreateStaffInformation", "Staff",new { id = user.UserId });
                    break;
            }
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
        public ActionResult MyProfile(int? id)
        {
            User user = new User();
            user = dbAccess.Users.Find(id);
            return View(user);
        }
        public ActionResult EditMyProfile(int? id)
        {
            User user = new User();
            user = dbAccess.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult EditMyProfile(User user)
        {
            var result = userServices.UpdateUser(user, user.UserId);
            return RedirectToAction("Index", "Home");
        }


    }
}