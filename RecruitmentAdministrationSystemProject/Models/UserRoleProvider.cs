using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Security;
using RecruitmentAdministrationSystemProject.Models;
using RecruitmentAdministrationSystemProject.Services;

namespace RecruitmentAdministrationSystemProject.Models
{
   
    public class UserRoleProvider : RoleProvider
    {
        RoleServices roleServices = new RoleServices();
        UserServices userServices = new UserServices();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var result = (from user in userServices.GetUsers()
                          join role in roleServices.GetRole()
                          on user.RoleId equals role.RoleId
                          where user.UserName == username
                          select role.RoleName).ToArray();
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userResult = (from user in userServices.GetUsers()
                             join roles in roleServices.GetRole()
                             on user.RoleId equals roles.RoleId
                             where user.UserName == username && roles.RoleName == roleName
                             select user).FirstOrDefault();
            if (userResult != null)
                return true;
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}