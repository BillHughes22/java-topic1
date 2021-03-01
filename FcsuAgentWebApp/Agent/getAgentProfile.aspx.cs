using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp;

namespace FcsuAgentWebApp.Agent
{
    public partial class getAgentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Context.Profile.SetPropertyValue("agentNumber", "A100");
            nameTextBox.Text = HttpContext.Current.Profile.GetPropertyValue("Name").ToString();
            agentTextBox.Text = HttpContext.Current.Profile.GetPropertyValue("agentNumber").ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Profile.SetPropertyValue("Name", nameTextBox.Text.Trim());
            HttpContext.Current.Profile.SetPropertyValue("agentNumber", agentTextBox.Text.Trim());
        }
    }
}