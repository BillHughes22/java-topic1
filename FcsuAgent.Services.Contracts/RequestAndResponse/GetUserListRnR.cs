using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FcsuAgent.Services.Contracts
{
    /// <summary>
    /// Request object containing all of the parameters passed to GetUserList() service call.
    /// </summary>
    [DataContract]
    public class GetUserListRequest
    {
        public GetUserListRequest()
        {
        }
    }

    /// <summary>
    /// Response object containing Success (of call), FailureMessage (if Success=false)
    /// </summary>
    [DataContract]
    public class GetUserListResponse
    {
        public GetUserListResponse()
        {
            Success = false;
            FailureMessage = string.Empty;
            UserList = new List<UserListInformation>();
        }

        /// <summary>
        /// If false, the call did not succeed and more information can be found in FailureMessage.
        /// </summary>
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }

        /// <summary>
        /// When Success = false, contains information about the failure suitable to show a user.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FailureMessage { get; set; }

        [DataMember(IsRequired = true)]
        public List<UserListInformation> UserList { get; set; }
    }
}
