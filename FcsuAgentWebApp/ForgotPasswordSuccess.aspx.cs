using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp
{
    public partial class ForgotPasswordSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.removeHomeInNavMenu();
            this.Master.addHeading("FCSU Member Portal");
            string uname= Request.QueryString["name"];
            if(!string.IsNullOrWhiteSpace(uname))
            {
                msg.Text = "Your Username : " + uname;
                
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx?heading=member");
        }
    }
}