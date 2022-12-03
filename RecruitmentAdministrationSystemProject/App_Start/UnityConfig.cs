using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace RecruitmentAdministrationSystemProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IDataAccessService<JobPost,int>, JobPostServices>();
            container.RegisterType<IDataAccessService<CandidateInfo, int>, CandidateInfoServices>();
            container.RegisterType<IDataAccessService<Company, int>,CompanyServices>();
            container.RegisterType<IDataAccessService<JobApplication, int>, JobApplicationServices>();
            container.RegisterType<IDataAccessService<Location, int>, LocationService>();
            container.RegisterType<IDataAccessService<Role, int>, RoleServices>();
            container.RegisterType<IDataAccessService<Skill, int>, SkillsServices>();
            container.RegisterType<IDataAccessService<Staff, int>, StaffService>();
            container.RegisterType<IDataAccessService<User, int>, UserServices>();
            container.RegisterType<IDataAccessService<Status, int>, StatusServices>();
            container.RegisterType<IDataAccessService<Interview, int>, InterviewServices>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}