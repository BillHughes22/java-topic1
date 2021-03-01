using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models
{
    public class UserRoleListDataModel
    {
        public UserRoleListDataModel()
        {
            UserPk = Guid.Empty;
            UserName = string.Empty;
            UserIsInRole = false;
        }

        public Guid UserPk { get; set; }
        public string UserName { get; set; }
        public bool UserIsInRole { get; set; }
    }
}