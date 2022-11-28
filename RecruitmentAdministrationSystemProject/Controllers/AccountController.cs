using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentAdministrationSystemProject.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.IO;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class AccountController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public ActionResult SignUp()
        {
            User user = new User();
            var roles = dbAccess.Roles.ToList().Skip(1).ToList();
            ViewBag.roles = roles;
            return View(user);
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
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
                        return RedirectToAction("CreateCandidateInfo", "Candidate", new { id = user.UserId });
                        break;
                    case 3:
                        return RedirectToAction("CreateCompanyInformation", "Company", new { id = user.UserId });
                        break;
                    case 4:
                        return RedirectToAction("CreateStaffInformation", "Staff", new { id = user.UserId });
                        break;
                }
                return RedirectToAction("SignUp");
            }
            else
            {
                return RedirectToAction("SignUp");
            }

        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Login(User user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = dbAccess.Users.ToList().Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(result.UserName, false);
                    Session["Uname"] = result.UserName.ToString();
                    Session["UID"] = result.UserId.ToString();
                    Session["User"] = result;
                    Session["Img"] = result.Img;
                    var role = (from userInfo in dbAccess.Users.ToList()
                                   join roles in dbAccess.Roles.ToList()
                                   on userInfo.RoleId equals roles.RoleId
                                   where userInfo.UserName == user.UserName
                                   select roles).FirstOrDefault();
                    Session["Role"] = role.RoleName.ToString();
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");

                    }
                }
                else
                {
                    ViewBag.LoginError = "Login Failed";
                    return RedirectToAction("login", "Account");

                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Uname"] = null;

            return RedirectToAction("Login");
        }
    }

}
