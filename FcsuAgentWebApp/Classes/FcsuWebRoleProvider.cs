using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CODE.Framework.Core.Utilities;
using CODE.Framework.Services.Client;
using FcsuAgent.Services.Contracts;
//using FcsuAgent.Services.Contracts;


namespace FcsuAgentWebApp.Classes
{
    public class FcsuWebRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {

            var roles = GetRolesForUser(username).ToList();
            var result = roles.Any(x => x == roleName);

            return result;
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                var response = new GetRolesForUserResponse();
                ServiceClient.Call<IFcsuServices>(x => response = x.GetRolesForUser(new GetRolesForUserRequest() { UserName = username }));

                if (response.Success)
                {
                    List<string> roleNames = new List<string>();
                    foreach (var role in response.Roles)
                    {
                        roleNames.Add(role.RoleName);
                    }
                    return roleNames.ToArray();
                }
                return new string[0];
            }
            catch (Exception exception)
            {
                LoggingMediator.Log(exception, LogEventType.Exception);
                return new string[0];
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { return "FCSU Agent Portal"; }
            set {/* Do Nothing */ }
        }
    }
}