using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class SaveUserRequest
    {
        public SaveUserRequest()
        {
            UserInfo = new UserInformation();
            Roles = new List<RoleInformation>();
        }
        [DataMember(IsRequired = true)]
        public UserInformation UserInfo { get; set; }

        [DataMember(IsRequired = true)]
        public List<RoleInformation> Roles { get; set; }
    }
}
