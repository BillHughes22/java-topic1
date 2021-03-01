using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class GetRolesRequest
    {

        [DataMember(IsRequired = true)]
        public RoleInformation RoleInfo { get; set; }

        public GetRolesRequest()
        {
            RoleInfo = new RoleInformation();
        }
    }
}
