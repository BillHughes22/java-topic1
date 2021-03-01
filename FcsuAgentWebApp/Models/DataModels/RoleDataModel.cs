using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models
{
    public class RoleDataModel
    {
        public Guid RolePk { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public bool UserIsInRole { get; set; }
    }
}