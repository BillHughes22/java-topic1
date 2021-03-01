using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CODE.Framework.Core.Utilities;
using CODE.Framework.Services.Client;
using FcsuAgent.Services.Contracts;

namespace FcsuAgentWebApp.Models.Admin
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
        }

        public RoleEditViewModel(Guid rolePk)
        {
            RolePk = rolePk;
            LoadData();
        }

        public Guid RolePk { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }

        private List<UserRoleListDataModel> _userRoleList = new List<UserRoleListDataModel>();
        public List<UserRoleListDataModel> UserRoleList
        {
            get { return _userRoleList; }
            set { _userRoleList = value; }
        }

        public void LoadData()
        {
            if (RolePk != Guid.Empty)
            {
                try
                {
                    var response = new GetRoleAndUsersResponse();
                    ServiceClient.Call<IFcsuServices>(x => response = x.GetRoleAndUsers(new GetRoleRequest
                        {
                            RolePk = RolePk
                        }));

                    if (response.Success)
                    {
                        Mapper.Map(response.RoleInfo, this);
                        foreach (var usr in response.UsersRoleList)
                        {
                            UserRoleList.Add(new UserRoleListDataModel
                                {
                                    UserName=usr.UserName,
                                    UserPk = usr.UserPk,
                                    UserIsInRole=usr.UserIsInRole
                                });
                        }
                    }
                    else
                    {
                        RoleName = response.FailureInformation;
                    }
                }
                catch (Exception exception)
                {
                    LoggingMediator.Log(exception, LogEventType.Exception);
                    RoleName = "Error getting user";
                }
            }
            else
            {
                RolePk = Guid.NewGuid();
                RoleName = string.Empty;
                Description = string.Empty;
                IsDisabled = false;
            }
        }

        public bool SaveUser(RoleEditViewModel view)
        {
            try
            {
                var response = new SaveRoleAndUsersResponse();
                var request = new SaveRoleAndUsersRequest();
                Mapper.Map(view, request.RoleInfo);
                foreach (var role in view.UserRoleList)
                {
                    request.UsersRoleList.Add(new UserRoleListInformation
                        {
                            UserIsInRole = role.UserIsInRole,
                            UserPk = role.UserPk
                        });
                }
                ServiceClient.Call<IFcsuServices>(x => response = x.SaveRoleAndUsers(request));

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
    }
}