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
    public partial class Authentication : System.Web.UI.Page
    {
        string agentoradminormemberordirector = string.Empty;
        string phone = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (User.IsInRole("member"))
            {
                this.Master.addHeading("FCSU Member Portal");
            }
            if (User.IsInRole("agent"))
            {
                this.Master.addHeading("FCSU Agent Portal");
            }
           
            if (User.IsInRole("director"))
            {
                this.Master.addHeading("FCSU Director Portal");
            }
            agentoradminormemberordirector = Session["Agent"] == null ? string.Empty :Session["Agent"].ToString();

            lblAgentOrMember.Text = agentoradminormemberordirector == "director" ? "Finddirectorfirstlogin.aspx":
              agentoradminormemberordirector == "admin" ? "../Admin/AgentList.aspx" :
              agentoradminormemberordirector == "member" ? "../Member/MemberMain.aspx": "../Agent/AgentMain.aspx";
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
                    if(agentoradminormemberordirector == "admin")
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