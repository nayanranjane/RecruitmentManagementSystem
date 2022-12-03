using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class CandidateInfoServices:IDataAccessService<CandidateInfo,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public CandidateInfoServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        async Task<bool> IDataAccessService<CandidateInfo, int>.Create(CandidateInfo entity)
        {
            try
            {
                var result = dbAccess.CandidateInfoes.Add(entity);
                var isDeleted = await dbAccess.SaveChangesAsync();
                if (isDeleted > 0)
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

        async Task<bool> IDataAccessService<CandidateInfo, int>.DeleteAsync(int id)
        {
            try
            {
                var candidateInfo =await dbAccess.CandidateInfoes.FindAsync(id);
                if (candidateInfo == null)
                {
                    throw new Exception("Info not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.CandidateInfoes.Remove(candidateInfo);
                    var isdeleted = await dbAccess.SaveChangesAsync();
                    if (isdeleted > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<CandidateInfo>> IDataAccessService<CandidateInfo, int>.GetDataAsync()
        {
            try
            {
                var candidateInfos = await dbAccess.CandidateInfoes.ToListAsync();
                return candidateInfos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<CandidateInfo> IDataAccessService<CandidateInfo, int>.GetDataAsync(int id)
        {
            try
            {
                var candidateInfo =await dbAccess.CandidateInfoes.FindAsync(id);
                if (candidateInfo != null)
                {
                    return candidateInfo;
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

        async Task<bool> IDataAccessService<CandidateInfo, int>.UpdateAsync(CandidateInfo entity, int id)
        {
            try
            {
                var candidateResult = dbAccess.CandidateInfoes.Find(id);
                if (candidateResult != null)
                {
                    candidateResult.SSC_Marks = entity.SSC_Marks;
                    candidateResult.SSC_College = entity.SSC_College;
                    candidateResult.SSC_PassingYear = entity.SSC_PassingYear;
                    candidateResult.HSC_Marks = entity.HSC_Marks;
                    candidateResult.HSC_College = entity.HSC_College;
                    candidateResult.HSC_PassingYear = entity.HSC_PassingYear;
                    candidateResult.UG_Marks = entity.UG_Marks;
                    candidateResult.UG_College = entity.UG_College;
                    candidateResult.UG_PassingYear = entity.UG_PassingYear;
                    candidateResult.Skill_1 = entity.Skill_1;
                    candidateResult.Skill_2 = entity.Skill_2;
                    candidateResult.Skill_3 = entity.Skill_3;
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