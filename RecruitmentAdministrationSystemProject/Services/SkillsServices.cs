using RecruitmentAdministrationSystemProject.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class SkillsServices
    {
        RecruitmentManagementSystemEntities dbAccess = new RecruitmentManagementSystemEntities();

        public IEnumerable<Skill> GetAllSkills()
        {
            var skillList = dbAccess.Skills.ToList();
            return skillList;
        }
    }
}