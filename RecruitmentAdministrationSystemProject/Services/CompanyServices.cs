using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class CompanyServices:IDataAccessService<Company,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public CompanyServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;   
        }

        async Task<Company> IDataAccessService<Company, int>.Create(Company entity)
        {
            try
            {
                var result = dbAccess.Companies.Add(entity);
                var isDeleted = await dbAccess.SaveChangesAsync();
                if (isDeleted > 0)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<bool> IDataAccessService<Company, int>.DeleteAsync(int id)
        {
            try
            {
                var company =await dbAccess.Companies.FindAsync(id);
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

        async Task<List<Company>> IDataAccessService<Company, int>.GetDataAsync()
        {
            try
            {
                var companyList = await dbAccess.Companies.ToListAsync();
                return companyList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<Company> IDataAccessService<Company, int>.GetDataAsync(int id)
        {
            try
            {
                var companyResult =await dbAccess.Companies.FindAsync(id);
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

        async Task<bool> IDataAccessService<Company, int>.UpdateAsync(Company entity, int id)
        {
            try
            {
                var companyResult = dbAccess.Companies.Find(id);
                if (companyResult != null)
                {
                    companyResult.CompanyUrl = entity.CompanyUrl;
                    companyResult.EmployeeCount = entity.EmployeeCount;
                    companyResult.AboutCompany = entity.AboutCompany;
                    companyResult.FoundDate = entity.FoundDate;
                }
                var isUpdated =await dbAccess.SaveChangesAsync();
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