using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models
{
    public class UserListDataModel
    {
        public UserListDataModel()
        {
            UserPk = Guid.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            IsDisabled = false;
            LastName = string.Empty;//1
            FirstName = string.Empty;//2
            MemberNumber = string.Empty;//3 
            AgentNumber = string.Empty;//3 added for userlist
            UpdatedDate = null;
        }
        public Guid UserPk { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }//2
        public string MemberNumber { get; set; }//3 
        public string AgentNumber { get; set; }//4 added for userlist

        public DateTime? UpdatedDate { get; set; }
    }
}