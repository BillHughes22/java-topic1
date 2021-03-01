using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CODE.Framework.Core.Utilities;
using CODE.Framework.Services.Client;
using FcsuAgent.Services.Contracts;

namespace FcsuAgentWebApp.Models.Admin
{
    public class RoleListViewModel
    {
        public RoleListViewModel()
        {
            LoadData();
        }

        private List<RoleDataModel> _roles = new List<RoleDataModel>();
        public List<RoleDataModel> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        private void LoadData()
        {
            try
            {
                var response = new GetRolesResponse();
                ServiceClient.Call<IFcsuServices>(x => response = x.GetRoles(new GetRolesRequest()));

                if (response.Success)
                {
                    foreach (var item in response.Roles)
                    {
                        var newRole = new RoleDataModel();
                        Mapper.Map(item, newRole);
                        Roles.Add(newRole);
                    }
                }
                else
                {
                    Roles.Add(new RoleDataModel { RoleName = response.FailureMessage });
                }
            }
            catch (Exception exception)
            {
                LoggingMediator.Log(exception, LogEventType.Exception);
                Roles.Add(new RoleDataModel { RoleName = "Error getting roles" });
            }
        }
    }
}