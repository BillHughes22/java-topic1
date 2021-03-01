using System;
using System.Runtime.Serialization;


namespace FcsuAgent.Services
{
    [DataContract]
    public class IsUserValidRequest
    {
        public IsUserValidRequest()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        [DataMember(IsRequired = true)]
        public string UserName { get; set; }

        [DataMember(IsRequired = true)]
        public string Password { get; set; }
    }

    ///<summary>Response object containing Success (of call), FailureMessage (if Success=false)</summary>
    [DataContract]
    public class IsUserValidResponse
    {
        public IsUserValidResponse()
        {
            Success = false;
            FailureMessage = string.Empty;
            UserId = Guid.Empty;
            AgentNumber = string.Empty;
//        MemberNumber = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        ///<summary>If false, the call did not succeed and more information can be found in FailureMessage.</summary>
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        ///<summary>When Success = false, contains information about the failure suitable to show a user.</summary>
        [DataMember(IsRequired = true)]
        public string FailureMessage { get; set; }

        ///<summary>If this is a valid user, we return the user's Id</summary>
        [DataMember(IsRequired = true)]
        public Guid UserId { get; set; }

        [DataMember(IsRequired = true)]
        public string AgentNumber { get; set; }

        //[DataMember(IsRequired = true)]
        //public string MemberNumber { get; set; }

        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = true)]
        public string LastName { get; set; }
    }
}