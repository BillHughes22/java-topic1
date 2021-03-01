using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FcsuAgentWebApp.Admin
{
    public partial class NoRoleTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("member"))
            {
                this.Master.addMemberMenu();
            }
            if (User.IsInRole("agent"))
            {
                this.Master.addAgentMenu();
            }
            if (User.IsInRole("admin"))
            {
                this.Master.addAdminMenu();
            }
            if (User.IsInRole("director"))
            {
                this.Master.addDirectorMenu();
            }

        }

        protected void GridViewNoRole_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewNoRole.SelectedRow;
            var userPk = GridViewNoRole.DataKeys[row.RowIndex].Value;
            Session["prevurl"] = "../admin/NoRoleTest.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }
    }
}