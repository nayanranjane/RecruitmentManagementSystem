using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class CompanyController : Controller
    {

        IDataAccessService<Company, int> companyService;
        IDataAccessService<User, int> userService;
        IDataAccessService<Role, int> roleService;

        public CompanyController(IDataAccessService<Company, int> companyService,IDataAccessService<User, int> userService,IDataAccessService<Role, int> roleService)
        {
            this.companyService = companyService;
            this.userService = userService;
            this.roleService = roleService;
        }

        public ActionResult CreateCompanyInformation(int id)
        {
            Company info = new Company() { UserId = id };
            return View(info);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCompanyInformation(Company info)
        {
            var isCreated =await companyService.Create(info);
            if (isCreated)
            {
                TempData["UserCreated"] = "UserCreated";
            }
            return RedirectToAction("Login", "Account");
        }



        async public Task<ActionResult> Index()
        {
            var Companies = (from user in (await userService.GetDataAsync())
                             join company in (await companyService.GetDataAsync())
                             on user.UserId equals company.UserId
                             join role in (await roleService.GetDataAsync())
                             on user.RoleId equals role.RoleId
                             where role.RoleName.ToLower()=="company".ToLower()
                             select new CompanyDetails
                             {
                                 UserId = user.UserId,
                                 CompanyId = company.CompanyId,
                                 Name = user.Name,
                                 MobileNo = user.MobileNo,
                                 Location = user.Location,
                                 Email = user.Email,
                                 Img = user.Img,
                                 CompanyUrl = company.CompanyUrl,
                                 EmployeeCount = company.EmployeeCount,
                                 AboutCompany = company.AboutCompany,
                                 FoundDate = company.FoundDate
                             }).ToList();
            ViewBag.Companies = Companies;
            return View();
        }

        public async Task<ActionResult> Index2()
        {
            var companies = (await companyService.GetDataAsync());
            return View(companies);

        }
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var result = await companyService.DeleteAsync(id);
            return RedirectToAction("Index","Company");
        }
    }
}