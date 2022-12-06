using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecruitmentAdministrationSystemProject.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.IO;
using RecruitmentAdministrationSystemProject.Services;
using System.Threading.Tasks;
using WebGrease.Css.Visitor;

namespace RecruitmentAdministrationSystemProject.Controllers
{

    public class AccountController : Controller
    {
       // RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        IDataAccessService<User, int> userService;
        IDataAccessService<Role, int> roleService;
        public AccountController( IDataAccessService<User, int> userService, IDataAccessService<Role, int> roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<ActionResult> SignUp()
        {
            User user = new User();
            var roles = (await roleService.GetDataAsync()).ToList().Skip(1).ToList();
            ViewBag.roles = roles;
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName); // .FleName Contain the name of the file with the directory
                    string extension = Path.GetExtension(user.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                    user.Img = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    user.ImageFile.SaveAs(filename);
                }
                else
                {
                    user.Img = "~/Image/" + "User.jfif";
                }
                var result = await userService.Create(user);
                switch (user.RoleId)
                {
                    case 2:
                        return RedirectToAction("CreateCandidateInfo", "Candidate", new { id = result.UserId });
                        break;
                    case 3:
                        return RedirectToAction("CreateCompanyInformation", "Company", new { id = result.UserId });
                        break;
                    case 4:
                        return RedirectToAction("CreateStaffInformation", "Staff", new { id = result.UserId });
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
            //User user = new User();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (true)
            {
                var result =( await userService.GetDataAsync()).ToList().Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(result.UserName, false);
                    Session["Uname"] = result.UserName.ToString();
                    Session["UID"] = result.UserId.ToString();
                    Session["User"] = result;
                    Session["Img"] = result.Img;

                    TempData["SuccessMessage"] = "Login SuccessFull";
                    return RedirectToAction("index", "home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Login Failed";
                    return RedirectToAction("login", "Account");

                }
            }
            return View();
        }
        public  ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Uname"] = null;

            return RedirectToAction("Login");
        }
        public async Task<JsonResult> IsUserExist(string username)
        {
            return Json(!(await userService.GetDataAsync()).Any(user => user.UserName == username), JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> isValidNumber(string MobileNo)
        {
            return Json(!(await userService.GetDataAsync()).Any(user => user.MobileNo == MobileNo), JsonRequestBehavior.AllowGet);
        }
    }

}