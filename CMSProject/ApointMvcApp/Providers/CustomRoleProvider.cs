using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
//using ApointMvcApp.Models;
using AppCore;
using Domain;

namespace ApointMvcApp.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private CoreHolder core;

        public CustomRoleProvider() : base(){ core = new CoreHolder();}

        public override string[] GetRolesForUser(string emailOrLogin)
        {
            var roles = new List<string>();
            var userByEmail = core.UserRepository.FindByEmail(emailOrLogin);
            var user = userByEmail != null ? userByEmail : core.UserRepository.Find(emailOrLogin);
            if (user != null)
            {
                foreach (var role in user.Roles)
                    roles.Add(role.Name);
            }
            return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                core.RoleRepository.Create(roleName);
                core.Submit();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public override bool IsUserInRole(string emailOrLogin, string roleName)
        {
            bool outputResult = false;
            var userByEmail = core.UserRepository.FindByEmail(emailOrLogin);
            var user = userByEmail != null ? userByEmail : core.UserRepository.Find(emailOrLogin);
            if (user != null)
            {
                var userRole = core.RoleRepository.ReadAll().Where(x => x.Name == roleName).FirstOrDefault();
                if (userRole != null && userRole.Name == roleName)
                    outputResult = true;
            }
            return outputResult;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                core.RoleRepository.Delete(core.RoleRepository.Find(roleName).ID);
                core.Submit();
                return true;
            }
            catch { throw new Exception("Ошибка - такой роли не существует"); }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            try
            {
                var roles = new List<string>();
                foreach (var role in core.RoleRepository.ReadAll())
                   if( !role.IsDeleted) roles.Add(role.Name);
                return roles.ToArray();
            }
            catch { return null; }

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
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