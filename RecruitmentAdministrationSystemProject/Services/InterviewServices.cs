using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class InterviewServices : IDataAccessService<Interview, int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public InterviewServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        async Task<Interview>  IDataAccessService<Interview, int>.Create(Interview entity)
        {
            try
            {
                var result = dbAccess.Interviews.Add(entity);
                var isAdded = await dbAccess.SaveChangesAsync();
                if (isAdded > 0)
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

        async Task<bool> IDataAccessService<Interview, int>.DeleteAsync(int id)
        {
            try
            {
                var interview = await dbAccess.Interviews.FindAsync(id);
                if (interview == null)
                {
                    throw new Exception("InterviewID found Enter Correct User ID");
                    return false;
                }
                else
                {
                    dbAccess.Interviews.Remove(interview);
                    await dbAccess.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<Interview>> IDataAccessService<Interview, int>.GetDataAsync()
        {
            try
            {
                var interviewList = await dbAccess.Interviews.ToListAsync();
                return interviewList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<Interview> IDataAccessService<Interview, int>.GetDataAsync(int id)
        {
            try
            {
                var interview = await dbAccess.Interviews.FindAsync(id);
                if (interview != null)
                {
                    return interview;
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

        async Task<bool> IDataAccessService<Interview, int>.UpdateAsync(Interview entity, int id)
        {
            try
            {
                var interview = await dbAccess.Interviews.FindAsync(id);
                if (interview != null)
                {
                    //interview.InterviewTime = entity.InterviewTime;
                    //interview.InterviewDate = entity.InterviewDate;
                    //interview.Description = entity.Description;
                    interview.StatusId = entity.StatusId;
                    interview.Remark = entity.Remark;
                    //interview.MettingId = entity.MettingId;
                }
                var isUpdated = await dbAccess.SaveChangesAsync();
                if (isUpdated > 0)
                {
                    return true;
                }
                return false;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}