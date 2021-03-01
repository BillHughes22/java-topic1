using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    public class RoleInformation
    {
        
        [DataMember(IsRequired = true)]
        public Guid RolePk { get; set; }

        [DataMember(IsRequired = true)]
        public string RoleName { get; set; }

        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsDisabled { get; set; }

        [DataMember(IsRequired = true)]
        public bool UserIsInRole { get; set; }

        public RoleInformation()
        {
            RolePk = Guid.NewGuid();
            RoleName = string.Empty;
            Description = string.Empty;
            IsDisabled = false;
            UserIsInRole = false;
        }
    
    }
}
