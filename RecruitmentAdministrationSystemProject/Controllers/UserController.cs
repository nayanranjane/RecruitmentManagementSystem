using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();
        //UserServices userServices = new UserServices(); 
        //RoleServices roleServices = new RoleServices();


        IDataAccessService<User, int> userService;
        public UserController(IDataAccessService<User, int> userService)
        {
            this.userService = userService;
        }

        public async Task<ActionResult> Index()
        {
            var Users = await userService.GetDataAsync();
            return View(Users);

        }


        //public ActionResult Create()
        //{
        //    User user = new User();
        //    var roles = roleServices.GetRole().Skip(1).ToList();
        //    ViewBag.roles = roles;
        //    return View(user);
        //}
        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    if (user.ImageFile!=null)
        //    {
        //    string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName); // .FleName Contain the name of the file with the directory
        //    string extension = Path.GetExtension(user.ImageFile.FileName);
        //    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
        //    user.Img = "~/Image/" + filename;
        //    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
        //    user.ImageFile.SaveAs(filename);
        //    }
        //    else
        //    {
        //        user.Img = "~/Image/" + "User.jfif";
        //    }
        //    var result = userServices.CreateUser(user);
        //    switch (user.RoleId)
        //    {
        //        case 2:
        //            return RedirectToAction("CreateCandidateInfo","Candidate", new { id = user.UserId });
        //            break;
        //        case 3:
        //            return RedirectToAction("CreateCompanyInformation","Company", new { id = user.UserId });
        //            break;
        //        case 4:
        //            return RedirectToAction("CreateStaffInformation", "Staff",new { id = user.UserId });
        //            break;
        //    }
        //    return RedirectToAction("Index");

        //}

        public async Task<ActionResult> DeleteUser(int id)
        {
            var isDeleted = await userService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DetailsUser(int id)
        {
            User result = new User();
            result = await userService.GetDataAsync(id);
            return View(result);
        }
        public ActionResult CandidateDetails(int? id)
        {
            return RedirectToAction("CandidateDetails", "Candidate", new {id=id});

        }
        public async Task<ActionResult> MyProfile(int id)
        {
            User user = new User();
            user = await userService.GetDataAsync(id);
            return View(user);
        }
        public async Task<ActionResult> EditMyProfile(int id)
        {
            User user = new User();
            user = await userService.GetDataAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> EditMyProfile(User user)
        {
            if (true)
            {
                var result = await userService.UpdateAsync(user, user.UserId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("EditMyProfile", new { id = user.UserId });
            }
        }


    }
}