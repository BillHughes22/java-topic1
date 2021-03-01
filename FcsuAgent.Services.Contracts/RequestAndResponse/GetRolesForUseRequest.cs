using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class GetRolesForUserRequest
    {

        [DataMember(IsRequired = true)]
        public string UserName { get; set; }

        public GetRolesForUserRequest()
        {
            UserName = string.Empty;
        }

    }
}
