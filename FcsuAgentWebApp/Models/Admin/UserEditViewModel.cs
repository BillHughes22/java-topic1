using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CODE.Framework.Core.Utilities;
using CODE.Framework.Services.Client;
using FcsuAgent.Services.Contracts;

namespace FcsuAgentWebApp.Models
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {
            SetPassword = null;
        }

        public UserEditViewModel(Guid userPk)
        {
            SetPassword = null;
            UserPk = userPk;
            LoadData();
        }

        public Guid UserPk { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public bool IsDisabled { get; set; }
        public string AgentNumber { get; set; }
        public string MemberNumber { get; set; }
        public string AgentMemberDescr { get; set; } //Naga 
        public string Phone { get; set; }//Naga  added 07/26/2020
        public string SetPassword { get; set; }
        private List<RoleDataModel> _userRoles = new List<RoleDataModel>();
        public List<RoleDataModel> UserRoles
        {
            get { return _userRoles; }
            set { _userRoles = value; }
        }

        public void LoadData()
        {
            if (UserPk != Guid.Empty)
            {
                try
                {
                    var response = new GetUserInformationResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetUserInformation(new GetUserInformationRequest
                        {
                            UserPk = UserPk
                        }));

                    if (response.Success)
                    {
                        Mapper.Map(response.User, this);
                        foreach (var role in response.Roles)
                        {
                            UserRoles.Add(new RoleDataModel
                                {
                                    Description = role.Description,
                                    IsDisabled = role.IsDisabled,
                                    RoleName = role.RoleName,
                                    UserIsInRole = role.UserIsInRole,
                                    RolePk = role.RolePk
                                });
                        }
                    }
                    else
                    {
                        UserName = response.FailureMessage;
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    UserName = "Error getting user";
                }
            }
            else
            {
                UserPk = Guid.NewGuid();
                UserName = string.Empty;
                FirstName = string.Empty;
                LastName = string.Empty;
                Email = string.Empty;
                Comments = string.Empty;
                IsDisabled = false;
                AgentNumber = string.Empty;
                MemberNumber = string.Empty;
                Phone = string.Empty;
                AgentMemberDescr = string.Empty;
                SetPassword = null;
                try
                {
                    var response = new GetRolesResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetRoles(new GetRolesRequest()));

                    if (response.Success)
                    {
                        foreach (var role in response.Roles)
                        {
                            UserRoles.Add(new RoleDataModel
                            {
                                Description = role.Description,
                                IsDisabled = role.IsDisabled,
                                RoleName = role.RoleName,
                                UserIsInRole = role.UserIsInRole,
                                RolePk = role.RolePk
                            });
                        }
                    }
                    else
                    {
                        UserName = response.FailureMessage;
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    UserName = "Error getting roles..";
                }
            }
        }

        public bool SaveUser(UserEditViewModel view)
        {
            try
            {
                var response = new SaveUserResponse();
                var request = new SaveUserRequest();
                Mapper.Map(view, request.UserInfo);
                foreach (var role in view.UserRoles)
                {
                    request.Roles.Add(new RoleInformation
                        {
                            Description = role.Description,
                            IsDisabled = role.IsDisabled,
                            RoleName = role.RoleName,
                            UserIsInRole = role.UserIsInRole,
                            RolePk = role.RolePk
                        });
                }
                ServiceClient.Call<IFcsuServices>(x => response = x.SaveUser(request));

                if (response.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                LoggingMediator.Log(exception, LogEventType.Exception);
                return false;
            }
        }
        //Purnima
        public bool DeleteUser(UserEditViewModel view)
        {
            try
            {
                var response = new DeleteUserResponse();
                var request = new DeleteUserRequest() { UserPk = view.UserPk, UserName = view.UserName };

                ServiceClient.Call<IFcsuServices>(x => response = x.DeleteUserResponse(request));

                if (response.Success)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception exception)
            {
                LoggingMediator.Log(exception, LogEventType.Exception);
                return false;
            }

        }
        //Purnima
    }
}