using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CODE.Framework.Core.Utilities;
using FcsuAgentWebApp.Models;
using System.Data.SqlClient;

namespace FcsuAgentWebApp.Admin
{
    public partial class UserEdit : System.Web.UI.Page
    {
        string prevUrl = string.Empty;
        int gridIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            gridIndex = Session["gridIndex"] == null ? 0 : Convert.ToInt32(Session["gridIndex"]);
            prevUrl = Session["prevurl"] == null ? string.Empty: string.Concat(Session["prevurl"].ToString(), "?myGVPageId=", gridIndex);
            
            if (IsPostBack) return ;
            var userQuery = Request.QueryString["userPk"];
            
            if (userQuery != null)
            {
                var userPk = Guid.Parse(userQuery);
                if (Guid.Parse(userQuery) == Guid.Empty) lblPassword.Text = "Password";
                var view = new UserEditViewModel(userPk);
                txtUserPk.Text = view.UserPk.ToString();
                txtUserName.Text = view.UserName;
                txtFirstName.Text = view.FirstName;
                txtLastName.Text = view.LastName;
                txtEmail.Text = view.Email;
                txtAgentNumber.Text = !String.IsNullOrWhiteSpace(view.AgentNumber) ? view.AgentNumber : "0";
                txtMemberNumber.Text = !String.IsNullOrWhiteSpace(view.MemberNumber) ? view.MemberNumber : "0";
                txtPhone.Text = !String.IsNullOrWhiteSpace(view.Phone) ? view.Phone : string.Empty;
               // cboAgentMemberDescr.Text = view.AgentMemberDescr;
                chkIsDisabled.Checked = view.IsDisabled;
                txtComments.Text = view.Comments;

                chkListRoles.DataSource = view.UserRoles;
                chkListRoles.DataBind();
                for (var i = 0; i < chkListRoles.Items.Count; i++)
                {
                    var role = view.UserRoles.FirstOrDefault(r => r.RolePk == Guid.Parse(chkListRoles.Items[i].Value));
                    if (role != null) chkListRoles.Items[i].Selected = role.UserIsInRole;
                }
            }
            else
            {
                txtUserName.Text = "No user id provided to lookup";
            }
        }

        protected void UserEdit_Cancel(object sender, CommandEventArgs e)
        {
            Response.Redirect(prevUrl);
          //  Response.Redirect("../admin/NewUserList.aspx");
        }

        protected void UserEdit_Save(object sender, CommandEventArgs e)
        {
            var view = new UserEditViewModel
                {
                    UserPk = Guid.Parse(txtUserPk.Text),
                    UserName = txtUserName.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    AgentNumber = txtAgentNumber.Text,
                    MemberNumber = txtMemberNumber.Text,
                    IsDisabled = chkIsDisabled.Checked,
                    Phone = txtPhone.Text,
                    Comments = txtComments.Text,
                    AgentMemberDescr = !string.IsNullOrWhiteSpace(txtAgentNumber.Text)?"Agent":"Member"
            };
            if (!string.IsNullOrEmpty(txtSetPassword.Text)) view.SetPassword = txtSetPassword.Text;

            for (var i = 0; i < chkListRoles.Items.Count; i++)
            {
                view.UserRoles.Add(new RoleDataModel
                    {
                        RolePk = Guid.Parse(chkListRoles.Items[i].Value),
                        UserIsInRole = chkListRoles.Items[i].Selected
                    });
            }

            var success = view.SaveUser(view);
            if (success && !chkIsDisabled.Checked)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                string delSqlStatement = $"delete from [UpdatedDateInfo] where UserName = '{txtUserName.Text}'  and isLoginFailed = 1";
                using (SqlConnection myConnection = new SqlConnection(connectionString))
                {
                    myConnection.Open();
                    SqlCommand myCommand1 = new SqlCommand(delSqlStatement, myConnection);

                    myCommand1.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            Session["gridIndex"] = gridIndex;
            if (success ) Response.Redirect(prevUrl);//"../admin/NewUserList.aspx" 09/28/2020

           
        }


        protected void UserEdit_Delete(object sender, CommandEventArgs e)
        {
            var view = new UserEditViewModel
            {
                UserPk = Guid.Parse(txtUserPk.Text),
                UserName = txtUserName.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                AgentNumber = txtAgentNumber.Text,
                Phone = txtPhone.Text,
                MemberNumber = txtMemberNumber.Text,
                IsDisabled = chkIsDisabled.Checked,
                Comments = txtComments.Text,
               // AgentMemberDescr = string.IsNullOrWhiteSpace(txtAgentNumber.Text) ? "Agent" : "Member"
            };
            var success = view.DeleteUser(view);
            if (success)
            {
                Response.Redirect(prevUrl);//"../admin/NewUserList.aspx" 09/28/2020
            }
            else
            {
                //we need to show the error message as "unable to delete user"
            }
        }
    }
}