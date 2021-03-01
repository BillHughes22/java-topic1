using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class GetRolesResponse
    {

        public GetRolesResponse()
        {
            Success = false;
            Roles = new List<RoleInformation>();
            FailureMessage = string.Empty;
        }

        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        [DataMember(IsRequired = true)]
        public List<RoleInformation> Roles { get; set; }

        [DataMember(IsRequired = true)]
        public string FailureMessage { get; set; }

    }
}
