using System;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class UserRoleListInformation
    {
        public UserRoleListInformation()
        {
            UserPk = Guid.Empty;
            UserName = string.Empty;
            UserIsInRole = false;
        }

        [DataMember(IsRequired = true)]
        public Guid UserPk { get; set; }
        [DataMember(IsRequired = true)]
        public string UserName { get; set; }
        [DataMember(IsRequired = true)]
        public bool UserIsInRole { get; set; }
        
    }
}