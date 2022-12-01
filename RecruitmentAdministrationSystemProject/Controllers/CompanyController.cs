﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RecruitmentAdministrationSystemProject.Models;

namespace RecruitmentAdministrationSystemProject.Controllers
{
    public class CompanyController : Controller
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public ActionResult CreateCompanyInformation(int id)
        {
            Company info = new Company() { UserId = id };
            return View(info);
        }
        [HttpPost]
        public ActionResult CreateCompanyInformation(Company info)
        {
            var result = dbAccess.Companies.Add(info);
            var isCreated = dbAccess.SaveChanges();
            if (isCreated > 0)
            {
                TempData["UserCreated"] = "UserCreated";
            }
            return RedirectToAction("Login", "Account");
        }



        public ActionResult Index()
        {
            var Companies = (from user in dbAccess.Users
                             join company in dbAccess.Companies
                             on user.UserId equals company.UserId
                             join role in dbAccess.Roles
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

        public ActionResult Index2()
        {
            var candidates = dbAccess.Companies.ToList();
            return View(candidates);

        }
        public ActionResult DeleteCompany(int? id)
        {
            var result = dbAccess.Users.Find(id);
            var isDeleted = dbAccess.Users.Remove(result);
            dbAccess.SaveChanges();
            return RedirectToAction("Index","Company");
        }
    }
}