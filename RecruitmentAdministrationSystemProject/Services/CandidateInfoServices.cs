using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class CandidateInfoServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();   

        public CandidateInfo CreateCandidateInfo(CandidateInfo info)
        {
            try
            {
                var result = dbAccess.CandidateInfoes.Add(info);
                dbAccess.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteCandidateInfo(int id)
        {
            try
            {
                var candidateInfo = dbAccess.CandidateInfoes.Find(id);
                if (candidateInfo == null)
                {
                    throw new Exception("Info not found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.CandidateInfoes .Remove(candidateInfo);
                    dbAccess.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<CandidateInfo> GetCandidateInfo()
        {
            try
            {
                var candidateInfo = dbAccess.CandidateInfoes.ToList();
                return candidateInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CandidateInfo GetCandidateInfo(int id)
        {
            try
            {
                var candidateInfo= dbAccess.CandidateInfoes.Find(id);
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
        public bool UpdateUser(CandidateInfo candidate, int id)
        {
            try
            {
                var candidateResult = dbAccess.CandidateInfoes.Find(id);
                if (candidateResult != null)
                {
                    candidateResult.SSC_Marks = candidate.SSC_Marks;
                    candidateResult.SSC_College = candidate.SSC_College;
                    candidateResult.SSC_PassingYear = candidate.SSC_PassingYear;
                    candidateResult.HSC_Marks = candidateResult.HSC_Marks;
                    candidateResult.HSC_College = candidate.HSC_College;
                    candidateResult.HSC_PassingYear = candidate.HSC_PassingYear;
                    candidateResult.UG_Marks = candidate.UG_Marks;
                    candidateResult.UG_College = candidate.UG_College;
                    candidateResult.UG_PassingYear = candidate.UG_PassingYear;
                    candidateResult.Skill_1 = candidate.Skill_1;
                    candidateResult.Skill_2 = candidate.Skill_2;
                    candidateResult.Skill_3 = candidate.Skill_3;
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