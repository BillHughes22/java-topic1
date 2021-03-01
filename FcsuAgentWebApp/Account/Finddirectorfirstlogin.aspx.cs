using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Account
{
    public partial class Finddirectorfirstlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("member"))
            {
                Response.Redirect("../member/memberMain.aspx");
            }
            else
            {
                int firstTymLoginInDirector = 0;
                MembershipUser username = Membership.GetUser();
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                using (SqlConnection myConnection1 = new SqlConnection(connectionString))
                {
                    string checkFirstDirectorLogin = $"select count(*) from [UpdatedDateInfo] where UserName = '{username}' ";
                    myConnection1.Open();
                    SqlCommand myCommand1 = new SqlCommand(checkFirstDirectorLogin, myConnection1);
                    SqlDataReader reader1 = myCommand1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {

                            firstTymLoginInDirector = (Int32)reader1[0];

                        }

                    }

                    myConnection1.Close();

                }

                if (firstTymLoginInDirector == 1)
                {
                    this.Master.disableLogin();
                    this.Master.removeHomeInNavMenu();
                    Response.Redirect("../Account/ChangePassword.aspx?directororAgent=nodirectormenu");

                }
                else
                {
                    this.Master.addDirectorMenu();
                    Response.Redirect("../Director/DirectorMenu.aspx");

                }
            }
                
        }
    }
}