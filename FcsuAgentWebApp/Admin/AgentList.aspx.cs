using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FcsuAgentWebApp.Models;
using System.Data;

namespace FcsuAgentWebApp.Admin
{
    public partial class AgentList : System.Web.UI.Page
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
            var view = new UserListViewModel();
            GridViewUsers.DataSource = view.Users;
            GridViewUsers.DataBind();
        }


        protected void GridViewUsers_RowSelected(object sender, EventArgs e)
        {
            var row = GridViewUsers.SelectedRow;
            var userPk = GridViewUsers.DataKeys[row.RowIndex].Value;
            Session["prevurl"] = "../admin/AgentList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + userPk.ToString());
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            var view = new UserListViewModel();
            GridViewUsers.DataSource = view.Users;

            this.GridViewUsers.DataBind();
        }
        protected void AddUser(object sender, CommandEventArgs e)
        {
            Session["prevurl"] = "../admin/AgentList.aspx";
            Response.Redirect("../admin/UserEdit.aspx?userPk=" + Guid.Empty.ToString());
        }

    }
}