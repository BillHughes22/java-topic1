using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace FcsuAgent.Services.Contracts
{
    [ServiceContract]
    public interface IFcsuServices
    {
        [OperationContract]
        GetRolesResponse GetRoles(GetRolesRequest request);

        [OperationContract]
        GetRoleResponse GetRole(GetRoleRequest request);

        [OperationContract]
        GetRoleAndUsersResponse GetRoleAndUsers(GetRoleRequest request);

        [OperationContract]
        GetRolesForUserResponse GetRolesForUser(GetRolesForUserRequest request);

        [OperationContract]
        GetUserInformationResponse GetUserInformation(GetUserInformationRequest request);

        [OperationContract]
        IsUserValidResponse IsUserValid(IsUserValidRequest request);

        [OperationContract]
        GetUserListResponse GetUserList(GetUserListRequest request);

        [OperationContract]
        SaveUserResponse SaveUser(SaveUserRequest request);

        [OperationContract]
        SaveRoleAndUsersResponse SaveRoleAndUsers(SaveRoleAndUsersRequest request);
        //Purnima
        [OperationContract]
        DeleteUserResponse DeleteUserResponse(DeleteUserRequest request);
        //Purnima
    }
}
