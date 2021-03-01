using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class SaveRoleAndUsersRequest
    {
        public SaveRoleAndUsersRequest()
        {
            RoleInfo = new RoleInformation();
            UsersRoleList = new List<UserRoleListInformation>();
        }

        [DataMember(IsRequired = true)]
        public RoleInformation RoleInfo { get; set; }

        [DataMember(IsRequired = true)]
        public List<UserRoleListInformation> UsersRoleList { get; set; }
    }

    [DataContract]
    public class SaveRoleAndUsersResponse
    {
        public SaveRoleAndUsersResponse()
        {
            Success = false;
            FailureInformation = string.Empty;
        }

        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        [DataMember(IsRequired = true)]
        public string FailureInformation { get; set; }
    }
}
