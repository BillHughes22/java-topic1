using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models;
using FcsuAgentWebApp.Models.Admin;

namespace FcsuAgentWebApp.Admin
{
    public partial class RoleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var userQuery = Request.QueryString["rolePk"];

            if (userQuery != null)
            {
                var rolePk = Guid.Parse(userQuery);
                var view = new RoleEditViewModel(rolePk);
                txtRolePk.Text = view.RolePk.ToString();
                txtRoleName.Text = view.RoleName;
                txtDescription.Text = view.Description;
                chkIsDisabled.Checked = view.IsDisabled;

                chkListUsers.DataSource = view.UserRoleList;
                chkListUsers.DataBind();
                for (var i = 0; i < chkListUsers.Items.Count; i++)
                {
                    var role = view.UserRoleList.FirstOrDefault(u => u.UserPk == Guid.Parse(chkListUsers.Items[i].Value));
                    if (role != null) chkListUsers.Items[i].Selected = role.UserIsInRole;
                }

            }
            else
            {
                txtRoleName.Text = "No role id provided to lookup";
            }
        }

        protected void RoleEdit_Cancel(object sender, CommandEventArgs e)
        {
            Response.Redirect("../admin/RoleList.aspx");
        }

        protected void RoleEdit_Save(object sender, CommandEventArgs e)
        {
            var view = new RoleEditViewModel
                {
                    RolePk = Guid.Parse(txtRolePk.Text),
                    RoleName = txtRoleName.Text,
                    Description = txtDescription.Text,
                    IsDisabled = chkIsDisabled.Checked,
                };

            for (var i = 0; i < chkListUsers.Items.Count; i++)
            {
                view.UserRoleList.Add(new UserRoleListDataModel
                    {
                        UserPk = Guid.Parse(chkListUsers.Items[i].Value),
                        UserIsInRole = chkListUsers.Items[i].Selected
                    });
            }

            var success = view.SaveUser(view);
            if (success ) Response.Redirect("../admin/RoleList.aspx");
        }
    }
    
}