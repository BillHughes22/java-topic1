using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp
{
    public partial class AuthenticationOut : System.Web.UI.Page
    {
        string phone = string.Empty;
        string uname = string.Empty;
        string unameorpswd = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.removeHomeInNavMenu();
            this.Master.addHeading("FCSU Member Portal");
            this.Master.disableLogin();
            uname = Session["forgotpassword"].ToString();
            unameorpswd = Session["unameorpswd"].ToString();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection myConnection1 = new SqlConnection(connectionString))
            {
                string checkFirstDirectorLogin = $"select phone from [user] where UserName = '{uname}' ";
                myConnection1.Open();
                SqlCommand myCommand1 = new SqlCommand(checkFirstDirectorLogin, myConnection1);
                SqlDataReader reader1 = myCommand1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {

                        phone = (string)reader1[0];

                    }

                }

                myConnection1.Close();
            }
            lbl.Text = phone.ToString();
            Label1.Text = uname;

            nav.Text = unameorpswd.Equals("uname") ?
                string.Concat("ForgotPasswordSuccess.aspx?name=",Label1.Text) :string.Concat("ChangePasswordOut.aspx?uname =",Label1.Text);

        }

        
    }
}