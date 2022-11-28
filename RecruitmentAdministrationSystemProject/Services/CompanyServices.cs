using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class CompanyServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();   

        public Company CreateCompany(Company company)
        {
            try
            {
                var result = dbAccess.Companies.Add(company);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteCompany(int id)
        {
            try
            {
                var company = dbAccess.Companies.Find(id);
                if (company == null)
                {
                    throw new Exception("Company not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Companies.Remove(company);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Company> GetCompany()
        {
            try
            {
                var companyList = dbAccess.Companies.ToList();
                return companyList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Company GetCompany(int id)
        {
            try
            {
                var companyResult = dbAccess.Companies.Find(id);
                if (companyResult != null)
                {
                    return companyResult;
                }
                else
                {
                    throw new Exception("Record not found");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateUser(Company company, int id)
        {
            try
            {
                var companyResult = dbAccess.Companies.Find(id);
                if (companyResult != null)
                {
                    companyResult.CompanyUrl = company.CompanyUrl;
                    companyResult.EmployeeCount = company.EmployeeCount;
                    companyResult.AboutCompany = company.AboutCompany;
                    companyResult.FoundDate = company.FoundDate;
                }
                var isUpdated = dbAccess.SaveChanges();
                if (isUpdated > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}