using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    [DataContract]
    public class DeleteUserRequest
    {
        public DeleteUserRequest()
        {
            UserName = string.Empty;
            UserPk = Guid.Empty;
        }
        [DataMember(IsRequired = true)]
        public string UserName { get; set; }

        [DataMember(IsRequired = true)]
        public Guid UserPk { get; set; }
    }
}
