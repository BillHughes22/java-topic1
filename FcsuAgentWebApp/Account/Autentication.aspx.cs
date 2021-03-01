using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FcsuAgentWebApp.Account
{
    public partial class Autenticate : System.Web.UI.Page
    {
        string agentoradmin = string.Empty;
        string phone = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            agentoradmin = Session["Agent"] == null ? string.Empty :Session["Agent"].ToString();
            lblAgentOrMember.Text = agentoradmin == string.Empty ? "Finddirectorfirstlogin.aspx":
              agentoradmin== "agent" ?"" : "../Admin/AgentList.aspx";
           if (lblAgentOrMember.Text == string.Empty)
            {
                this.Master.removeHomeInNavMenu();
                Session["directororAgent"] = "director";
            }
              
                MembershipUser username = Membership.GetUser();
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                {
                    string checkFirstDirectorLogin = $"select phone from [user] where UserName = '{username}' ";
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

               
         
            

           
            lblPh.Text = phone.ToString();
            if(lblAgentOrMember.Text != string.Empty)
            {
                if (string.IsNullOrWhiteSpace(lblPh.Text))
                {
                    if(agentoradmin=="admin")
                    {
                        Response.Redirect("../Admin/AgentList.aspx");
                    }
                    else
                    {
                        Response.Redirect("../Admin/AgentMain.aspx");
                    }

                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(lblPh.Text))
                {

                    Response.Redirect("../Account/Error.aspx");

                }

            }
         
        }
    }
}