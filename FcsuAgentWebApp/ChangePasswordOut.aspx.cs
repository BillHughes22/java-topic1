using CODE.Framework.Core.Utilities;
using FcsuAgentWebApp.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp
{
    public partial class ChangePasswordOut : System.Web.UI.Page
    {
        public static byte[] PasswordKey1 = new byte[] { 48, 110, 143, 130, 105, 128, 147, 129, 163, 130, 122, 11, 43, 18, 181, 22, 72, 42, 124, 164, 243, 152, 19, 200 };
        int uodatedornot = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        string UName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.removeHomeInNavMenu();
            this.Master.addHeading("FCSU Member Portal");
            this.Master.disableLogin();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            UName = url.Split('?')[1].Split('=')[1];
            NewPasswordLabel.Text = "Password:" + "<i> (minimum 8 characters - 1 uppercase, 1 lowercase, 1 special character and 1 number) </i> "; 
            //CurrentPasswordLabel.Visible = false;
            //CurrentPassword.Visible = false;
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
           
            string Password = SecurityHelper.EncryptString(NewPassword.Text,
                                                                        PasswordKey1);
        string insertSqlStatement = $"update [User] set password='{Password}' where UserName = '{UName}'";
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(insertSqlStatement, myConnection);

                uodatedornot =  myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            if(uodatedornot==1)
            {
                Response.Redirect("~/ForgotPasswordSuccess.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Error.aspx");
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx?heading=member", true);
        }
    }
}