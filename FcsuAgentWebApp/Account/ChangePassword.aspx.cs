using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Reflection;
using FcsuAgentWebApp.Classes;
using System.Data.SqlClient;

namespace FcsuAgentWebApp.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string directororAgent = string.Empty; 
        protected void Page_Load(object sender, EventArgs e)
        {
            directororAgent = Request.QueryString["directororAgent"];
            if (User.IsInRole("member"))
            {
                Label heading = (Label)ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("head");
                heading.Text = "Passwords are required to be a minimum of 8 characters in length and should contain at least one uppercase, one lowercase, one number and one special character.";
                ////TextBox chngePswd = (TextBox)ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("NewPassword");
                ////chngePswd.Visible = false;
                RegularExpressionValidator RemoveRegExpPswd = ((RegularExpressionValidator)(ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("RegularExpressionValidator1")));
                RemoveRegExpPswd.Enabled = true;
                
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("agent"))
            {
                RegularExpressionValidator RemoveRegExpPswd = ((RegularExpressionValidator)(ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("RegularExpressionValidator1")));
                RemoveRegExpPswd.Enabled = false;
                this.Master.addAgentMenu();

            }
           
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }
            if (User.IsInRole("director"))

            {
                RegularExpressionValidator RemoveRegExpPswd = ((RegularExpressionValidator)(ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("RegularExpressionValidator1")));
                RemoveRegExpPswd.Enabled = false;
                if (directororAgent == "nodirectormenu")
                {
                    this.Master.disableLogin();
                    this.Master.removeHomeInNavMenu();
                }

                this.Master.addDirectorMenu();
            }
            this.ChangeUserPassword.ChangingPassword += new LoginCancelEventHandler(ChangeUserPassword_ChangingPassword);


        }

        void ChangeUserPassword_ChangingPassword(object sender, LoginCancelEventArgs e)
        {
           
            // call membership provider
            MembershipUser mu = Membership.GetUser(this.ChangeUserPassword.UserName);
            var customMembershipProvider = (FcsuWebMembershipProvider)Membership.Provider;
          
            bool changed = customMembershipProvider.ChangePassword(mu.UserName, this.ChangeUserPassword.CurrentPassword, this.ChangeUserPassword.NewPassword);
            if (changed)
            {
                e.Cancel = true;
                MethodInfo successMethodInfo = this.ChangeUserPassword.GetType().GetMethod("PerformSuccessAction", BindingFlags.NonPublic | BindingFlags.Instance);
                successMethodInfo.Invoke(this.ChangeUserPassword, new object[] { "", "", this.ChangeUserPassword.NewPassword });
               
            }
            

        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            if (directororAgent == "nodirectormenu")
            {
                MembershipUser username = Membership.GetUser();
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                {
                    string checkFirstDirectorLogin = $"delete from [UpdatedDateInfo] where UserName = '{username}' ";
                    myConnection1.Open();
                    SqlCommand myCommand1 = new SqlCommand(checkFirstDirectorLogin, myConnection1);
                    myCommand1.ExecuteNonQuery();
                    FormsAuthentication.SignOut();
                    Session["Login"] = null;
                    Response.Redirect("../Account/Login.aspx?heading=director");
                    myConnection1.Close();

                }

            }

          
            else if (User.IsInRole("member"))
            {
                Response.Redirect("../Member/memberMain.aspx");
            }
            else
            {
                Response.Redirect("../admin/adminMain.aspx");
            }


        }

        
    }
}
