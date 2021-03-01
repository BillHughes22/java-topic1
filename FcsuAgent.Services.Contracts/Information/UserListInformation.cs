using System;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    /// <summary>
    /// Data for a UserList in the system.
    /// </summary>
    [DataContract]
    public class UserListInformation
    {
        public UserListInformation()
        {
            UserPk = Guid.Empty;
        }

        /// <summary>
        /// Unique Id (primary key) for the UserList.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid UserPk { get; set; }

        [DataMember(IsRequired = true)]
        public string UserName { get; set; }
        [DataMember(IsRequired = true)]
        public string Email { get; set; }
        [DataMember(IsRequired = true)]
        public bool IsDisabled { get; set; }
        [DataMember(IsRequired = true)]
        public string MemberNumber { get; set; }
        [DataMember(IsRequired = true)]
        public string LastName { get; set; }
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }
        [DataMember(IsRequired = true)]
        public string AgentNumber { get; set; }
    }
}