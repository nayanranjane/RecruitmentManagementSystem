using RecruitmentAdministrationSystemProject.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class SkillsServices:IDataAccessService<Skill,int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public SkillsServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

  

        Task<Skill> IDataAccessService<Skill, int>.Create(Skill entity)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Skill, int>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<List<Skill>> IDataAccessService<Skill, int>.GetDataAsync()
        {
            try
            {
                var skillList = await dbAccess.Skills.ToListAsync();
                return skillList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        Task<Skill> IDataAccessService<Skill, int>.GetDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Skill, int>.UpdateAsync(Skill entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}