using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class GetRoleRequest
    {
        public GetRoleRequest()
        {
            RolePk = Guid.Empty;
        }

        [DataMember(IsRequired = true)]
        public Guid RolePk { get; set; }
    }
    
    [DataContract]
    public class GetRoleResponse
    {
        public GetRoleResponse()
        {
            RoleInfo = new RoleInformation();
            Success = false;
            FailureInformation = string.Empty;
        }

        [DataMember(IsRequired = true)]
        public RoleInformation RoleInfo { get; set; }

        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        [DataMember(IsRequired = true)]
        public string FailureInformation { get; set; }
        
    }

    [DataContract]
    public class GetRoleAndUsersResponse
    {
        public GetRoleAndUsersResponse()
        {
            RoleInfo = new RoleInformation();
            UsersRoleList = new List<UserRoleListInformation>();
            Success = false;
            FailureInformation = string.Empty;
        }

        [DataMember(IsRequired = true)]
        public RoleInformation RoleInfo { get; set; }

        [DataMember(IsRequired = true)]
        public List<UserRoleListInformation> UsersRoleList { get; set; }

        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        [DataMember(IsRequired = true)]
        public string FailureInformation { get; set; }

        
    }
}
