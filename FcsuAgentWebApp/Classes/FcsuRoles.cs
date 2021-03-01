using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FcsuAgent.Services.Contracts;
using CODE.Framework.Services.Client;
using CODE.Framework.Core.Utilities;

namespace FcsuAgentWebApp.Classes
{
    public class FcsuRoles : Roles
    {
        private RoleInformation _roles;
        
        public FcsuRoles()
        {
            _roles = new RoleInformation();
        }
        public FcsuRoles(RoleInformation roles)
        {
            _roles = roles;
        }




    }
}